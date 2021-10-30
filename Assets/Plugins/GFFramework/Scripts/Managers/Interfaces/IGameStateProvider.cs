using GFFramework.Enums;

namespace GFFramework
{
    public interface IGameStateProvider
    {
        public void LoadInitGameState(IGetProvidersRegister reg);
        public void LoadGameState(GameStateKey gameStateKey);
    }
}