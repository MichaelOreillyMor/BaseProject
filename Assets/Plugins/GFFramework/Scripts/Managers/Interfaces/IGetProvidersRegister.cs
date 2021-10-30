namespace GFFramework
{
    public interface IGetProvidersRegister
    {
        public IGameStateProvider GetGameState();
        public ISceneProvider GetScene();
        public IDataProvider GetData();
        public IUIProvider GetUI();
        public IInputProvider GetInput();
        public IPlayerProvider GetPlayer();
        public IPoolProvider GetPool();
    }
}