namespace GFFramework
{
    public interface ISetProvidersRegister
    {
        public void SetGameState(IGameStateProvider provider);
        public void SetScene(ISceneProvider provider);
        public void SetData(IDataProvider provider);
        public void SetUI(IUIProvider provider);
        public void SetInput(IInputProvider provider);
        public void SetPlayer(IPlayerProvider provider);
        public void SetPool(IPoolProvider provider);
    }
}
