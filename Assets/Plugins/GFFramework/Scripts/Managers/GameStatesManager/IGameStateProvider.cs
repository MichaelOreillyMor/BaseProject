using GFF.Enums;
using GFF.RegProviders;

namespace GFF.GameStatesMan
{
    public interface IGameStateProvider
    {
        public void LoadInitGameState(IGetService reg);
        public void LoadGameState(GameStateKey gameStateKey);
        public void LoadPrevGameState();
    }
}