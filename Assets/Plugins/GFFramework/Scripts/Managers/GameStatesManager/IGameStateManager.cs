using GFF.Enums;
using GFF.ServiceLocators;

namespace GFF.GameStatesMan
{
    public interface IGameStateManager
    {
        public void LoadInitGameState(IGetService reg);
        public void LoadGameState(GameStateKey gameStateKey);
        public void LoadPrevGameState();
    }
}