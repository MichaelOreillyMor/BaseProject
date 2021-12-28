using GFF.GameStatesMan;
using GFF.GameStatesMan.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GFF.Editor
{
    public class GameStatesEnumProccessor : EnumProccessor
    {
        private const string managerPath = "Assets/Plugins/GFFramework/Prefabs/Managers/GameStateManager.prefab";

        public GameStatesEnumProccessor()
        {
            folderPath = "Assets/Datas/GameStates/";
            assetExtension = ".asset";
            assetType = typeof(BaseGameState);
        }

        protected override List<UnityEngine.Object> GetSaveAssets()
        {
            GameStateManager gameStateManager = (GameStateManager)AssetDatabase.LoadAssetAtPath(managerPath, typeof(GameStateManager));
         
            if (gameStateManager) 
            {
                return gameStateManager.GetGameStatesEditor().ToList<UnityEngine.Object>();
            }

            return null;
        }

        protected override void SetSaveAssets(List<UnityEngine.Object> assets)
        {
            GameStateManager gameStateManager = (GameStateManager)AssetDatabase.LoadAssetAtPath(managerPath, typeof(GameStateManager));

            if (gameStateManager)
            {        
                BaseGameState[] gameStates = Array.ConvertAll(assets.ToArray(), a => (BaseGameState)a);
                gameStateManager.SetGameStatesEditor(gameStates);

                EditorUtility.SetDirty(gameStateManager);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
