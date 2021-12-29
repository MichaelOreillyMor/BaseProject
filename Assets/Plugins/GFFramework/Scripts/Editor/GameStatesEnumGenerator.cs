﻿using GFF.GameStatesMan;
using GFF.GameStatesMan.GameStates;

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
            "GameStates", "GFF.GameStatesMan", typeof(BaseGameState))
        {
            prefabPath = "Assets/Plugins/GFFramework/Prefabs/Managers/GameStateManager.prefab";
        }

        protected override List<UnityEngine.Object> GetSaveAssets()
        {
            GameStateManager gameStateManager = (GameStateManager)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameStateManager));
         
            if (gameStateManager) 
            {
                return gameStateManager.GetGameStatesEditor().ToList<UnityEngine.Object>();
            }

            return null;
        }

        protected override void SetSaveAssets(List<UnityEngine.Object> assets)
        {
            GameStateManager gameStateManager = (GameStateManager)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameStateManager));

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
