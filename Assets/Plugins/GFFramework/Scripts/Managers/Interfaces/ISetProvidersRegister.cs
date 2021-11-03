namespace GFFramework
{
    public interface ISetProvidersRegister
    {
        public IGameStateProvider GameStateProv { set; }
        public ISceneProvider SceneProv { set; }
        public IDataProvider DataProv { set; }
        public IUIProvider UIProv { set; }
        public IInputProvider InputProv { set; }
        public IPlayerProvider PlayerProv { set; }
        public IPoolProvider PoolProv { set; }
    }
}
