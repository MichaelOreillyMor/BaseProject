using GFFramework.Enums;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFFramework.GameStates
{
    /// <summary>
    /// Handles the different game´s states, it´s a States pattern controlled by this class
    /// </summary>
    public class GameStateManager : BaseGameManager, IGameStateProvider
    {
        //WIP: Unity cant serialize a Assets diccionary so I have to do this
        [SerializeField]
        private BaseGameState[] gameStatesToLoad;

        //In C# since Enums do not implement IEquatable, they'll be casted to object (boxing) in order to compare the keys Object.Equals().
        //Thanks to il2cpp this is not happening 
        private Dictionary<GameStateKey, BaseGameState> gameStates;

        [SerializeField]
        private GameStateKey initGameState;

        private BaseGameState currentGameState;
        private BaseGameState prevGameState;

        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.GameStateProv = this;
            LoadGameStates();

            Debug.Log("Setup GameStateManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup GameStateManager");
        }

        #endregion

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

        public void LoadInitGameState(IGetProvidersRegister reg)
        {
            BaseGameState.SetProvidersRegister(reg);
            LoadGameState(initGameState);
        }

        public void LoadGameState(GameStateKey gameStateKey)
        {
            BaseGameState gameState = GetGameState(gameStateKey);

            if (gameState)
            {
                if (currentGameState)
                {
                    prevGameState = currentGameState;

                    Debug.Log("Unsetup " + name);
                    prevGameState.Unsetup();
                }

                currentGameState = gameState;

                Debug.Log("Setup " + name);
                gameState.Setup();
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
            return gameState;
        }

        private void Update()
        {
            if (currentGameState)
            {
                currentGameState.Update();
            }
        }
    }
}
