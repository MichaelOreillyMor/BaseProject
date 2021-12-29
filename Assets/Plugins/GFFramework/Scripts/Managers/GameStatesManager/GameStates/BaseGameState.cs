using GFF.CoroutinesMan;
using GFF.GameStatesMan.Keys;
using GFF.ServiceLocators;
using GFF.Utils;

using UnityEngine;

namespace GFF.GameStatesMan.GameStates
{
    /// <summary>
    /// Base state that changes the state of the game, e.g: UI to show, input received, player´s  representation...
    /// </summary>
    public abstract class BaseGameState : ScriptableObject
    {
        /// <summary>
        /// Unique ID that identifies this GameState
        /// </summary>
        public GameStateKey Key => key;

        [SerializeField, ReadOnly]
        private GameStateKey key;

        [SerializeField]
        private GameStateKey nextGameState;

        protected IGameStateManager gameStateMan;
        private ICoroutinesManager coroutinesMan;

        #region Setup/Unsetup methods

        /// <summary>
        /// The GameState gets the references to the providers that needs. 
        /// It happens at the beginning of the game so it´s easy to see if someone provider is missing.
        /// </summary>
        public void SetServices(IGetService serviceLocator)
        {
            if (serviceLocator != null)
            {
                gameStateMan = serviceLocator.GetService<IGameStateManager>();
                OnSetServices(serviceLocator);
            }
        }

        /// <summary>
        /// Here any derived GameState gets the references to the providers that needs.
        /// </summary>
        protected abstract void OnSetServices(IGetService reg);

        /// <summary>
        /// Entry method where the dependencies of the GameState components are resolved (Dependency Injection Composition root)
        ///the components can also start to listen to the events that they need here.
        /// </summary>
        public void Setup() 
        {
            //I did it to let space to add more logic to the BaseGameState initialization
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

            if (coroutinesMan != null)
            {
                coroutinesMan.StopAllGameStateCoroutines();
            }
        }


        protected abstract void OnPreUnsetup();

        #endregion

        public abstract void Update();

        protected void LoadNextGameState() 
        {
            if (nextGameState != GameStateKey.None)
            {
                gameStateMan.LoadGameState(nextGameState);
            }
        }

        protected void LoadPrevGameState()
        {
            gameStateMan.LoadPrevGameState();
        }

        #region Editor methods

        public void SetKey_Editor(GameStateKey key)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                this.key = key;
            }
#endif
        }

        #endregion
    }
}
