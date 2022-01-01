using GFF.Generated;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEditor;
using UnityEngine;

namespace GFF.Editor
{
    public abstract class BaseAssetsEnumGenerator
    {
        private static bool FileEventsLocked;

        private const int MAX_NUM_NULL = 0;

        private readonly string folderPath;
        private readonly string prefabEnumPath;
        private readonly string assetExtension;
        private readonly string enumTitle;
        private readonly string namespaceTitle;

        private readonly Type assetType;
        private EnumGenerator enumGenerator;

        public BaseAssetsEnumGenerator(string folderPath, string prefabEnumPath, string assetExtension, 
            string enumTitle, string namespaceTitle, Type assetType)
        {
            this.folderPath = folderPath;
            this.prefabEnumPath = prefabEnumPath;
            this.assetExtension = assetExtension;
            this.assetType = assetType;
            this.enumTitle = enumTitle;
            this.namespaceTitle = namespaceTitle;

            enumGenerator = new EnumGenerator();
        }

        #region Get/Set EnumAssets methods

        protected abstract void PreSetSavedEnumAssets(List<UnityEngine.Object> assets);

        private void SetSavedEnumAssets(List<UnityEngine.Object> assets)
        {
            LockFileEvents();
            PreSetSavedEnumAssets(assets);
            IGenAssetsEnum gameStateManager = (IGenAssetsEnum)AssetDatabase.LoadAssetAtPath(prefabEnumPath, typeof(IGenAssetsEnum));

            if (gameStateManager != null)
            {
                gameStateManager.SetAssetsEnum_Editor(assets);

            }
            EditorUtility.SetDirty(gameStateManager.GetAssetObject_Editor());
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorApplication.update += WaitUnlockFileEvents;
        }

        private List<UnityEngine.Object> GetSavedEnumAssets()
        {
            IGenAssetsEnum gameStateManager = (IGenAssetsEnum)AssetDatabase.LoadAssetAtPath(prefabEnumPath, typeof(IGenAssetsEnum));

            if (gameStateManager != null)
            {
                return gameStateManager.GetAssetsEnum_Editor();
            }

            return null;
        }

        #endregion

        #region Lock Files mehtods

        private void WaitUnlockFileEvents()
        {
            if (!EditorApplication.isCompiling)
            {
                UnlockFileEvents();
                EditorApplication.update -= WaitUnlockFileEvents;
            }
            else
            {
                Debug.Log("Waiting to UnlockFileEvents...");
            }
        }

        protected void LockFileEvents()
        {
            Debug.Log("FileEvents Locked");
            FileEventsLocked = true;
        }

        protected void UnlockFileEvents()
        {
            FileEventsLocked = false;
            Debug.Log("FileEvents Unlocked");
        }

    #endregion

        private void OnCreated(string path) 
        {
            UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(path, assetType);

            if (asset)
            {
                Debug.Log(enumTitle + " Enum creating...");

                List<UnityEngine.Object> assets = GetSavedEnumAssets();
                int numNull = assets.FindAll(a => a == null).Count;

                if (!assets.Contains(asset))
                {
                    if (!HasAssetsEnumName(assets, asset))
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
                        SetSavedEnumAssets(assets);
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
                Debug.Log(enumTitle + " Enum deleting...");

                List<UnityEngine.Object> assets = GetSavedEnumAssets();
                int deletedIndex = assets.IndexOf(asset);

                if (deletedIndex != -1)
                {
                    assets[deletedIndex] = null;
                    GenerateAssetsEnum(assets);
                    SetSavedEnumAssets(assets);
                }
                else
                {
                    Debug.LogError(enumTitle + " Enum not generated, cant find deleted asset: " + asset.name);
                }
            }
        }

        private void OnNameChange()
        {
            Debug.Log(enumTitle + " Enum file name changing...");

            List<UnityEngine.Object> asssets = GetSavedEnumAssets();
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
            List<string> enumNames = GetEnumNames(assets);
            return enumNames.Contains(enumName);
        }

        private bool HasAssetsEnumName(string enumName)
        {
            List<UnityEngine.Object> assets = GetSavedEnumAssets();
            List<string> enumNames = GetEnumNames(assets);
            return enumNames.Contains(enumName);
        }

        private List<string> GetEnumNames(List<UnityEngine.Object> assets)
        {
            string enumName;
            int countNull = 1;
            List<string> enumNames = new List<string>(assets.Count);

            foreach (UnityEngine.Object asset in assets)
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
            if (!FileEventsLocked && IsValidPath(assetPath))
            {
                OnCreated(assetPath);
            }
        }

        public void OnPreFileDeleted(string assetPath)
        {
            if (!FileEventsLocked && IsValidPath(assetPath))
            {
                OnDeleted(assetPath);
            }
        }

        public void OnPostFileMoved(string sourcePath, string destinationPath)
        {
            if (!FileEventsLocked)
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
        }

        #endregion
    }
}
