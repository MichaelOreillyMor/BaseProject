namespace GFFramework
{
    public interface IGetProvidersRegister
    {
        public IGameStateProvider GameStateProv { get; }
        public ISceneProvider SceneProv { get; }
        public IDataProvider DataProv { get; }
        public IUIProvider UIProv { get; }
        public IInputProvider InputProv { get; }
        public IPlayerProvider PlayerProv { get; }
        public IPoolProvider PoolProv { get; }
        public ICameraProvider CameraProv { get; }
        public ICoroutinesProvider CoroutinesProv { get; }
    }
}