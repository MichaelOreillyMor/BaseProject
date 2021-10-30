using GFFramework.Enums;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFFramework
{
    public class GameStateManager : BaseGameManager, IGameStateProvider
    {
        //Odin cant serialize a ScriptableObjects diccionary so I have to do this
        [SerializeField]
        private BaseGameState[] gameStatesToLoad;
        private Dictionary<GameStateKey, BaseGameState> gameStates;

        [SerializeField]
        private GameStateKey initGameState;

        private BaseGameState currentGameState;
        private BaseGameState prevGameState;

        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SetGameState(this);

            LoadGameState();

            Debug.Log("Setup GameStateManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup GameStateManager");
        }

        #endregion

        private void LoadGameState()
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
                    prevGameState.Unsetup();
                }

                currentGameState = gameState;
                gameState.Setup();
            }
         }

        public void ReturnPrevGameState()
        {
            if (prevGameState)
            {
                if (currentGameState)
                {
                    currentGameState.Unsetup();
                }

                BaseGameState auxGameState = currentGameState;
                currentGameState = prevGameState;
                prevGameState = auxGameState;

                currentGameState.Setup();
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
