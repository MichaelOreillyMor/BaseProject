using GFFramework.Enums;
using UnityEngine;

namespace GFFramework.GameStates
{
    /// <summary>
    /// Base state that changes the state of the game, e.g: UI to show, input received, player´s  representation...
    /// It keeps also a static reference to the diferent providers (Managers)
    /// </summary>
    public abstract class BaseGameState : ScriptableObject
    {
        protected static IGameStateProvider gameStateProv { get; private set; }
        protected static ISceneProvider sceneProv { get; private set; }
        protected static IDataProvider dataProv { get; private set; }
        protected static IUIProvider uiProv { get; private set; }
        protected static IInputProvider inputProv { get; private set; }
        protected static IPlayerProvider playerProv { get; private set; }
        protected static IPoolProvider poolProv { get; private set; }

        static public void SetProvidersRegister(IGetProvidersRegister register)
        {
            gameStateProv = register.GetGameState();
            sceneProv = register.GetScene();
            dataProv = register.GetData();
            uiProv = register.GetUI();
            inputProv = register.GetInput();
            playerProv = register.GetPlayer();
            poolProv = register.GetPool();
        }

        [SerializeField]
        private GameStateKey key;
        public GameStateKey Key => key;

        [SerializeField]
        protected GameStateKey nextGameState;

        public abstract void Setup();
        public abstract void Unsetup();
        public abstract void Update();

        protected void LoadNextState() 
        {
            if (nextGameState != GameStateKey.None)
            {
                gameStateProv.LoadGameState(nextGameState);
            }
        }
    }
}
