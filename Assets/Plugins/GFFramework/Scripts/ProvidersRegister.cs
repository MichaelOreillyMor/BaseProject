namespace GFFramework
{
    public class ProvidersRegister : IGetProvidersRegister, ISetProvidersRegister
    {
        public IGameStateProvider GetGameState() => gameStateProvider;
        public ISceneProvider GetScene() => sceneProvider;
        public IDataProvider GetData() => dataProvider;
        public IUIProvider GetUI() => uiProvider;
        public IInputProvider GetInput() => inputProvider;
        public IPlayerProvider GetPlayer() => playerProvider;
        public IPoolProvider GetPool() => poolProvider;

        public void SetGameState(IGameStateProvider provider) => gameStateProvider = provider;
        public void SetScene(ISceneProvider provider) => sceneProvider = provider;
        public void SetData(IDataProvider provider) => dataProvider = provider;
        public void SetUI(IUIProvider provider) => uiProvider = provider;
        public void SetInput(IInputProvider provider) => inputProvider = provider;
        public void SetPlayer(IPlayerProvider provider) => playerProvider = provider;
        public void SetPool(IPoolProvider provider) => poolProvider = provider;

        private IGameStateProvider gameStateProvider;
        private ISceneProvider sceneProvider;
        private IDataProvider dataProvider;
        private IUIProvider uiProvider;
        private IInputProvider inputProvider;
        private IPlayerProvider playerProvider;
        private IPoolProvider poolProvider;
    }
}