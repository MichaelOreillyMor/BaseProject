﻿using GFF.GameStatesMan.Keys;
using GFF.GameStatesMan.GameStates;
using GFF.ServiceLocators;

using System;
using UnityEngine;

namespace GFF.GameStatesMan
{
    /// <summary>
    /// Handles the different game´s states, it´s a States pattern controlled by this class
    /// </summary>
    public class GameStateManager : BaseGameManager, IGameStateManager
    {
        [SerializeField, Disable]
        private BaseGameState[] gameStates;

        private BaseGameState currentGameState;
        private BaseGameState prevGameState;

        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetupCallback)
        {
            SetService(serviceLocator);

            onNextSetupCallback?.Invoke();
        }

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<IGameStateManager>(this);
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup GameStateManager");
        }

        #endregion

        #region Load GameStates methods

        public void InitGameStates(GameStateKey initGameState, IGetService serviceLocator)
        {
            SetGameStatesServices(serviceLocator);

            Debug.Log("Init GameState: " + initGameState);
            LoadGameState(initGameState);
        }

        /// <summary>
        /// Resolves the references to the providers needed in every GameState.
        /// </summary>
        private void SetGameStatesServices(IGetService serviceLocator)
        {
            if (gameStates != null)
            {
                for (int i = 0; i < gameStates.Length; i++)
                {
                    BaseGameState gs = gameStates[i];

                    if (gs)
                    {
                        gs.SetServices(serviceLocator);
                    }
                }
            }
        }

        public void LoadGameState(GameStateKey gameStateKey)
        {
            if (gameStateKey != GameStateKey.None)
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
                    Debug.LogError("In GameState: " + gameStateKey.ToString() + " Not found");
                }
            }
            else
            {
                Debug.LogError("InGameState is GameStateKey.None");
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
            int indexGameState = ((int)gameStateKey) - 1;

            if (gameStates != null && indexGameState < gameStates.Length)
            {
                return gameStates[indexGameState];
            }

            Debug.Log(gameStateKey.ToString() + " not found");
            return null;
        }

        #endregion

        private void Update()
        {
            if (currentGameState)
            {
                currentGameState.Update();
            }
        }

        #region Editor methods

        public void SetGameStates_Editor(BaseGameState[] gameStates) 
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                this.gameStates = gameStates;
            }
#endif
        }

        public BaseGameState[] GetGameStates_Editor()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return gameStates;
            }
#endif

            return null;
        }

        #endregion
    }
}
