﻿using GFF.Enums;
using GFF.GameStatesMan.GameStates;
using GFF.RegProviders;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFF.GameStatesMan
{
    /// <summary>
    /// Handles the different game´s states, it´s a States pattern controlled by this class
    /// </summary>
    public class GameStateManager : BaseGameManager, IGameStateProvider
    {
        //WIP: Unity cant serialize a Assets diccionary so I have to do this
        [SerializeField]
        private BaseGameState[] gameStatesToLoad;

        //In C# since Enums do not implement IEquatable, they'll be casted to object (boxing) in order to compare the keys Object.Equals()
        //Thanks to il2cpp this is not happening 
        private Dictionary<GameStateKey, BaseGameState> gameStates;

        [SerializeField]
        private GameStateKey initGameState;

        private BaseGameState currentGameState;
        private BaseGameState prevGameState;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetupCallback)
        {
            reg.GameStateProv = this;
            LoadGameStates();

            Debug.Log("Setup GameStateManager");
            onNextSetupCallback?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup GameStateManager");
        }

        private void LoadGameStates()
        {
            if (gameStatesToLoad != null)
            {
                gameStates = new Dictionary<GameStateKey, BaseGameState>();
                for (int i = 0; i < gameStatesToLoad.Length; i++)
                {
                    BaseGameState gs = gameStatesToLoad[i];
                    gameStates.Add(gs.Key, gs);
                }
            }
        }

        #endregion

        #region Load GameStates methods

        public void LoadInitGameState(IGetProvidersRegister reg)
        {
            SetGameStatesProviders(reg);
            LoadGameState(initGameState);
        }

        /// <summary>
        /// Resolves the references to the providers needed in every GameState.
        /// </summary>
        private void SetGameStatesProviders(IGetProvidersRegister reg)
        {
            if (gameStatesToLoad != null)
            {
                for (int i = 0; i < gameStatesToLoad.Length; i++)
                {
                    BaseGameState gs = gameStatesToLoad[i];
                    gs.SetProviders(reg);
                }
            }
        }

        public void LoadGameState(GameStateKey gameStateKey)
        {
            BaseGameState nextGameState = GetGameState(gameStateKey);

            if (nextGameState)
            {
                if (currentGameState)
                {
                    prevGameState = currentGameState;

                    Debug.Log("Out GameState: " + prevGameState.name);
                    prevGameState.Unsetup();
                }

                currentGameState = nextGameState;

                Debug.Log("In GameState: " + currentGameState.name);
                currentGameState.Setup();
            }
            else 
            {
                Debug.LogError("GameState: " + gameStateKey.ToString() + " Not found");
            }
         }

        public void LoadPrevGameState()
        {
            if (prevGameState)
            {
                LoadGameState(prevGameState.Key);
            }
        }

        private BaseGameState GetGameState(GameStateKey gameStateKey)
        {
            BaseGameState gameState;
            gameStates.TryGetValue(gameStateKey, out gameState);

            if (gameState == null)
            {
                Debug.Log(gameStateKey.ToString() + " not found");
            }

            return gameState;
        }

        #endregion

        private void Update()
        {
            if (currentGameState)
            {
                currentGameState.Update();
            }
        }
    }
}