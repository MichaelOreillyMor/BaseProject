using GFF.GameStatesMan;
using GFF.GameStatesMan.GameStates;
using GFF.GameStatesMan.Keys;

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace GFF.Editor
{
    public class GameStatesEnumGenerator : BaseAssetsEnumGenerator
    {
        private readonly string prefabPath;

        public GameStatesEnumGenerator() : base("Assets/Datas/GameStates/", ".asset",
            "GameStateKey", "GFF.GameStatesMan.Keys", typeof(BaseGameState))
        {
            prefabPath = "Assets/Plugins/GFFramework/Prefabs/Managers/GameStateManager.prefab";
        }

        protected override List<UnityEngine.Object> GetSaveAssets()
        {
            GameStateManager gameStateManager = (GameStateManager)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameStateManager));
         
            if (gameStateManager) 
            {
                return gameStateManager.GetGameStates_Editor().ToList<UnityEngine.Object>();
            }

            return null;
        }

        protected override void SetSaveAssets(List<UnityEngine.Object> assets)
        {
            GameStateManager gameStateManager = (GameStateManager)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameStateManager));

            if (gameStateManager)
            {        
                BaseGameState[] gameStates = Array.ConvertAll(assets.ToArray(), a => (BaseGameState)a);
                gameStateManager.SetGameStates_Editor(gameStates);

                for (int i = 0; i < assets.Count; i++)
                {
                    UnityEngine.Object asset = assets[i];

                    if (asset != null)
                    {
                        if (asset is BaseGameState gameState)
                        {
                            gameState.SetKey_Editor((GameStateKey)(i + 1)); 
                            EditorUtility.SetDirty(gameState);
                        }
                    }
              
                }

                EditorUtility.SetDirty(gameStateManager);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
