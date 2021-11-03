using GFFramework.Enums;
using UnityEngine;

namespace GFFramework.GameStates
{
    /// <summary>
    /// Base state that changes the state of the game, e.g: UI to show, input received, player´s  representation...
    /// It keeps also a static reference to the providers (Managers)
    /// </summary>
    public abstract class BaseGameState : BaseInitScriptObj
    {
        protected static IGetProvidersRegister reg { get; private set; }
        static public void SetProvidersRegister(IGetProvidersRegister register) => reg = register;

        [SerializeField]
        private GameStateKey key;
        public GameStateKey Key => key;

        [SerializeField]
        protected GameStateKey nextGameState;

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
