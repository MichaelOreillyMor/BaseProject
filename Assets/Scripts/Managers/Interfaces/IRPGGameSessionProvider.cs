using GFFramework;
using RPGGame.BoardCells;
using RPGGame.Units;

namespace RPGGame
{
    public interface IRPGGameSessionProvider : IGameSessionProvider
    {
        public void InitSession(Board board, UnitState[] player1Units, UnitState[] player2Units);
    }
}