using GFFramework.GameStates;

namespace GFFramework
{
    public interface ISetProvidersRegister
    {
        public IGameStateProvider GameStateProv { set; }
        public ISceneProvider SceneProv { set; }
        public IDataProvider DataProv { set; }
        public IUIProvider UIProv { set; }
        public IInputProvider InputProv { set; }
        public IPoolProvider PoolProv { set; }
        public ICameraProvider CameraProv { set; }
        public ICoroutinesProvider CoroutinesProv { set; }
        public IGameSessionProvider GameSessionProv { set; }
    }
}
