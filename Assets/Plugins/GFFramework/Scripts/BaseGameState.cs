using GFFramework.Enums;

using UnityEngine;

namespace GFFramework.GameStates
{
    /// <summary>
    /// Base state that changes the state of the game, e.g: UI to show, input received, player´s  representation...
    /// It keeps also a static reference to the providers (Managers)
    /// </summary>
    public abstract class BaseGameState : ScriptableObject
    {
        protected static IGetProvidersRegister Reg { get; private set; }
        static public void SetProvidersRegister(IGetProvidersRegister register) => Reg = register;

        public GameStateKey Key => key;

        [SerializeField]
        private GameStateKey key;

        [SerializeField]
        private GameStateKey nextGameState;

        /// <summary>
        /// Entry method where the dependencies of the GameState components are resolved (Dependency Injection Composition root)
        ///the components can also start to listen to the events that they need here.
        /// </summary>
        public abstract void Setup();

        /// <summary>
        /// Exit method where any resource taken by the game state is disposed (e.g: PoolMember)
        /// and the components of the GameState stop listening to events
        /// </summary>
        public abstract void Unsetup();

        public abstract void Update();

        protected void LoadNextGameState() 
        {
            if (nextGameState != GameStateKey.None)
            {
                Reg.GameStateProv.LoadGameState(nextGameState);
            }
        }

        protected void LoadPrevGameState()
        {
            Reg.GameStateProv.LoadPrevGameState();
        }
    }
}
