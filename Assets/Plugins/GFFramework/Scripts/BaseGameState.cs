﻿using GFFramework.Enums;

using UnityEngine;

namespace GFFramework.GameStates
{
    /// <summary>
    /// Base state that changes the state of the game, e.g: UI to show, input received, player´s  representation...
    /// It keeps also a static reference to the providers (Managers)
    /// </summary>
    public abstract class BaseGameState : ScriptableObject
    {
        protected static IGetProvidersRegister reg { get; private set; }
        static public void SetProvidersRegister(IGetProvidersRegister register) => reg = register;

        public GameStateKey Key => key;

        [SerializeField]
        private GameStateKey key;

        [SerializeField]
        private GameStateKey nextGameState;

        public abstract void Setup();
        public abstract void Unsetup();
        public abstract void Update();

        protected void LoadNextGameState() 
        {
            if (nextGameState != GameStateKey.None)
            {
                reg.GameStateProv.LoadGameState(nextGameState);
            }
        }

        protected void LoadPrevGameState()
        {
            reg.GameStateProv.LoadPrevGameState();
        }
    }
}
