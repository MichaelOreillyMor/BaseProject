using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEditor;
using UnityEngine;

//TO-DO: Crear folders de folderPath si no existen
namespace GFF.Editor
{
    public abstract class BaseAssetsEnumGenerator
    {
        private const int MAX_NUM_NULL = 0;

        private readonly string folderPath;
        private readonly string assetExtension;
        private readonly string enumTitle;
        private readonly string namespaceTitle;

        private readonly Type assetType;

        private EnumGenerator enumGenerator;

        public BaseAssetsEnumGenerator(string folderPath, string assetExtension, 
            string enumTitle, string namespaceTitle, Type assetType)
        {
            this.folderPath = folderPath;
            this.assetExtension = assetExtension;
            this.assetType = assetType;
            this.enumTitle = enumTitle;
            this.namespaceTitle = namespaceTitle;

            enumGenerator = new EnumGenerator();
        }

        protected abstract List<UnityEngine.Object> GetSaveAssets();

        protected abstract void SetSaveAssets(List<UnityEngine.Object> assets);

        private void OnCreated(string path) 
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            if (asset)
            {
                Debug.Log(enumTitle + " Enum created...");

                List<UnityEngine.Object> assets = GetSaveAssets();
                int numNull = assets.FindAll(a => a == null).Count;

                if (!assets.Contains(asset))
                {
                    if (HasAssetsEnumName(assets, asset))
                    {
                        if (numNull > MAX_NUM_NULL)
                        {
                            int firstNull = assets.FindIndex(a => a == null);

                            if (firstNull != -1)
                            {
                                assets[firstNull] = asset;
                            }
                        }
                        else
                        {
                            assets.Add(asset);
                        }

                        GenerateAssetsEnum(assets);
                        SetSaveAssets(assets);
                    }
                    else
                    {
                        Debug.LogError(enumTitle + " Enum not generated, already contains name: " + asset.name);
                    }
                }
                else
                {
                    Debug.LogError(enumTitle + " Enum not generated, already contains: " + asset.name);
                }
            }
        }

        private void OnDeleted(string path)
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            if (asset)
            {
                Debug.Log(enumTitle + " Enum deleted...");

                List<UnityEngine.Object> asssets = GetSaveAssets();
                int deletedIndex = asssets.IndexOf(asset);

                if (deletedIndex != -1)
                {
                    asssets[deletedIndex] = null;
                    GenerateAssetsEnum(asssets);
                }
                else
                {
                    Debug.LogError(enumTitle + " Enum not generated, cant find deleted asset: " + asset.name);
                }
            }
        }

        private void OnNameChange()
        {
            Debug.Log(enumTitle + " Enum file name change...");

            List<UnityEngine.Object> asssets = GetSaveAssets();
            GenerateAssetsEnum(asssets);
        }

        private void GenerateAssetsEnum(List<UnityEngine.Object> asssets)
        {
            List<string> enumNames = GetEnumNames(asssets);
            enumGenerator.CreateEnum(enumTitle, namespaceTitle, enumNames);
        }

        private bool HasAssetsEnumName(List<UnityEngine.Object> assets, UnityEngine.Object asset)
        {
            string enumName = GetAssetEnumName(asset);
            return HasAssetsEnumName(assets, enumName);
        }

        private bool HasAssetsEnumName(List<UnityEngine.Object> assets, string enumName)
        {
            List<string> enumNames = GetEnumNames(assets);
            return enumNames.Contains(enumName);
        }

        private bool HasAssetsEnumName(string enumName)
        {
            List<UnityEngine.Object> assets = GetSaveAssets();
            List<string> enumNames = GetEnumNames(assets);
            return enumNames.Contains(enumName);
        }

        private List<string> GetEnumNames(List<UnityEngine.Object> asssets)
        {
            string enumName;
            int countNull = 1;
            List<string> enumNames = new List<string>(asssets.Count);

            foreach (UnityEngine.Object asset in asssets)
            {
                if (asset)
                {
                    enumName = GetAssetEnumName(asset);
                } 
                else
                {
                    enumName = "NULL_" + countNull;
                    countNull++;
                }

                enumNames.Add(enumName);
            }

            return enumNames;
        }
        private bool IsValidPath(string assetPath)
        {
            return assetPath.StartsWith(folderPath) && assetPath.EndsWith(assetExtension);
        }

        private string GetAssetEnumName(UnityEngine.Object assset)
        {
            return Regex.Replace(assset.name, "[^a-zA-Z0-9]", string.Empty);
        }

        private string GetAssetEnumName(string assset)
        {
            return Regex.Replace(assset, "[^a-zA-Z0-9]", string.Empty);
        }

        private string GetPathEnumName(string asssetPath)
        {
            return GetAssetEnumName(Path.GetFileNameWithoutExtension(asssetPath));
        }

        #region Files methods

        public void OnPostFileCreated(string assetPath)
        {
            if (IsValidPath(assetPath))
            {
                OnCreated(assetPath);
            }
        }

        public void OnPreFileDeleted(string assetPath)
        {
            if (IsValidPath(assetPath))
            {
                OnDeleted(assetPath);
            }
        }

        public void OnPostFileMoved(string sourcePath, string destinationPath)
        {
            bool sourceValid = IsValidPath(sourcePath);
            bool destinationValid = IsValidPath(destinationPath);
            bool hasEnumName = HasAssetsEnumName(GetPathEnumName(sourcePath));

            if (sourceValid && destinationValid && hasEnumName)
            {
                OnNameChange();
            }
            else if (sourceValid && hasEnumName)
            {
                OnDeleted(sourcePath);
            }
            else if (destinationValid && !hasEnumName)
            {
                OnCreated(destinationPath);
            }
        }

        #endregion
    }
}
