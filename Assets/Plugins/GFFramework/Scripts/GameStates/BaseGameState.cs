using GFFramework.Enums;

using System;
using System.Collections;
using UnityEngine;

namespace GFFramework.GameStates
{
    /// <summary>
    /// Base state that changes the state of the game, e.g: UI to show, input received, player´s  representation...
    /// It keeps also a static reference to the providers (Managers)
    /// </summary>
    public abstract class BaseGameState : ScriptableObject
    {
        private static IGetProvidersRegister register;
        static public void SetProvidersRegister(IGetProvidersRegister reg) => register = reg;

        /// <summary>
        /// Unique ID that identifies this GameState
        /// </summary>
        public GameStateKey Key => key;

        [SerializeField]
        private GameStateKey key;

        [SerializeField]
        private GameStateKey nextGameState;

        protected IGameStateProvider gameStateProv;
        private ICoroutinesProvider coroutinesProv;

        #region Setup/Unsetup methods

        /// <summary>
        /// Method executed before Setup(), here any derived GameState gets the references to the providers that needs.
        /// </summary>
        protected abstract void SetProviders(IGetProvidersRegister reg);

        /// <summary>
        /// Entry method where the dependencies of the GameState components are resolved (Dependency Injection Composition root)
        ///the components can also start to listen to the events that they need here.
        /// </summary>
        public void Setup() 
        {
            if (register != null)
            {
                gameStateProv = register.GameStateProv;
                SetProviders(register);
            }

            OnPostSetup();
        }

        protected abstract void OnPostSetup();

        /// <summary>
        /// Exit method where any resource taken by the game state is disposed (e.g: PoolMember)
        /// and the components of the GameState stop listening to events
        /// </summary>
        public void Unsetup()
        {
            OnPreUnsetup();

            if (coroutinesProv != null)
            {
                coroutinesProv.StopAllGameStateCoroutines();
            }
        }


        protected abstract void OnPreUnsetup();

        #endregion

        public abstract void Update();

        protected void LoadNextGameState() 
        {
            if (nextGameState != GameStateKey.None)
            {
                gameStateProv.LoadGameState(nextGameState);
            }
        }

        protected void LoadPrevGameState()
        {
            gameStateProv.LoadPrevGameState();
        }

        #region Coroutines methods

        protected void StartCoroutine(IEnumerator coroutine) 
        {
            if (coroutinesProv != null)
            {
                coroutinesProv.StartGameStateCoroutine(coroutine);
            }
            else 
            {
                Debug.LogError("CoroutinesProvider not added, please add one");
            }
        }

        protected void StopCoroutine(IEnumerator coroutine)
        {
            if (coroutinesProv != null)
            {
                coroutinesProv.StopGameStateCoroutine(coroutine);
            }
            else
            {
                Debug.LogError("CoroutinesProvider not added, please add one");
            }
        }

        protected IEnumerator DelayAction(Action action, int delay)
        {
            IEnumerator delayCoroutine = null;

            if (coroutinesProv != null)
            {
                delayCoroutine = coroutinesProv.StartDelayGameStateAction(action, delay);
            }
            else
            {
                Debug.LogError("CoroutinesProvider not added, please add one");
            }

            return delayCoroutine;
        }

        #endregion
    }
}
