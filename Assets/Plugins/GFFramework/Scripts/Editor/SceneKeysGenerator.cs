using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GFF.Editor
{
    public class SceneKeysGenerator : BaseAssetsEnumGenerator
    {
        #region Singleton

        public static SceneKeysGenerator EditorInstance
        {
            get
            {
                if (editorInstance == null)
                {
                    editorInstance = new SceneKeysGenerator();
                }

                return editorInstance;
            }
        }

        private static SceneKeysGenerator editorInstance;

        #endregion

        public SceneKeysGenerator() : base("Assets/Scenes/", 
            "Assets/Plugins/GFFramework/Prefabs/Managers/SceneManager.prefab", 
            ".unity", "SceneKey", "GFF.ScenesMan.Keys", typeof(SceneAsset))
        {

        }

        protected override void PreSetSavedEnumAssets(List<Object> assets)
        {
  
        }
    }
}