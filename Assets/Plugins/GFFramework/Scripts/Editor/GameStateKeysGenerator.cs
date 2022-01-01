using GFF.GameStatesMan.GameStates;
using GFF.GameStatesMan.Keys;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GFF.Generated.Editor
{
    public class GameStateKeysGenerator : BaseAssetKeysGenerator
    {
        #region Singleton

        public static GameStateKeysGenerator EditorInstance
        {
            get
            {
                if (editorInstance == null)
                {
                    editorInstance = new GameStateKeysGenerator();
                }

                return editorInstance;
            }
        }

        private static GameStateKeysGenerator editorInstance;

        #endregion

        public GameStateKeysGenerator() : base("Assets/Datas/GameStates/", 
            "Assets/Plugins/GFFramework/Prefabs/Managers/GameStateManager.prefab", 
            ".asset", "GameStateKey", "GFF.GameStatesMan.Keys", typeof(BaseGameState))
        {

        }

        protected override void PreSetSavedEnumAssets(List<Object> assets)
        {
            for (int i = 0; i < assets.Count; i++)
            {
                Object asset = assets[i];

                if (asset != null)
                {
                    if (asset is BaseGameState gameState)
                    {
                        gameState.SetKey_Editor((GameStateKey)(i + 1));
                        EditorUtility.SetDirty(gameState);
                    }
                }

            }
        }
    }
}
