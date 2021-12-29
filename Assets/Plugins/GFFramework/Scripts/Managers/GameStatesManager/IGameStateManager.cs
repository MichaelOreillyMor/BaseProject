using GFF.Enums;
using GFF.ServiceLocators;

namespace GFF.GameStatesMan
{
    public interface IGameStateManager
    {
        public void InitGameStates(GameStateKey initGameState, IGetService serviceLocator);
        public void LoadGameState(GameStateKey gameStateKey);
        public void LoadPrevGameState();
    }
}