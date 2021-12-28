using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEditor;

//TO-DO: Crear folders de folderPath si no existen
namespace GFF.Editor
{
    public abstract class BaseAssetsEnumGenerator
    {
        private const int MAX_NUM_NULL = 0;

        private readonly string folderPath;
        private readonly string assetExtension;
        private readonly Type assetType;

        public BaseAssetsEnumGenerator(string folderPath, string assetExtension, Type assetType)
        {
            this.folderPath = folderPath;
            this.assetExtension = assetExtension;
            this.assetType = assetType;
        }

        protected abstract List<UnityEngine.Object> GetSaveAssets();

        protected abstract void SetSaveAssets(List<UnityEngine.Object> assets);

        private void OnCreated(string path) 
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            if (asset)
            {
                List<UnityEngine.Object> asssets = GetSaveAssets();
                int numNull = asssets.FindAll(a => a == null).Count;

                if (numNull > MAX_NUM_NULL)
                {
                    int firstNull = asssets.FindIndex(a => a == null);

                    if (firstNull != -1)
                    {
                        asssets[firstNull] = asset;
                    }
                }
                else 
                {
                    asssets.Add(asset);
                }

                GenerateAssetsEnum(asssets);
                SetSaveAssets(asssets);
            }
        }

        private void OnDeleted(string path)
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            if (asset)
            {
                List<UnityEngine.Object> asssets = GetSaveAssets();
                GenerateAssetsEnum(asssets);
            }
        }

        private void OnNameChange()
        {
            List<UnityEngine.Object> asssets = GetSaveAssets();
            GenerateAssetsEnum(asssets);
        }

        private void GenerateAssetsEnum(List<UnityEngine.Object> asssets)
        {
            List<string> enumNames = GetEnumNames(asssets);

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
                    enumName = GetEnumName(asset);
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

        private string GetEnumName(UnityEngine.Object assset)
        {
            return Regex.Replace(assset.name, "[^a-zA-Z0-9]", string.Empty);
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

            if (sourceValid && destinationValid)
            {
                OnNameChange();
            }
            else if (sourceValid)
            {
                OnDeleted(sourcePath);
            }
            else if (destinationValid)
            {
                OnCreated(destinationPath);
            }
        }

        #endregion
    }
}
