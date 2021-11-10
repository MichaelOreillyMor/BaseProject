namespace GFFramework
{
    public class ProvidersRegister : IGetProvidersRegister, ISetProvidersRegister
    {
        public IGameStateProvider GameStateProv { get; set; }
        public ISceneProvider SceneProv { get; set; }
        public IDataProvider DataProv { get; set; }
        public IUIProvider UIProv { get; set; }
        public IInputProvider InputProv { get; set; }
        public IPlayerProvider PlayerProv { get; set; }
        public IPoolProvider PoolProv { get; set; }
        public ICameraProvider CameraProv { get; set; }
        public ICoroutinesProvider CoroutinesProv { get; set; }
    }
}