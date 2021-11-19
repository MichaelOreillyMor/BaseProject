using GFFramework.GameSession;
using RPGGame.BoardCells;
using RPGGame.Units;

namespace RPGGame.GameSession
{
    public class RPGGameSessionManager : GameSessionManager, IRPGGameSessionProvider
    {
        private Board board;
        private UnitState[] player1Units;
        private UnitState[] player2Units;

        public void InitSession(Board board, UnitState[] player1Units, UnitState[] player2Units) 
        {
            if (TryInitSession())
            {
                this.board = board;
                this.player1Units = player1Units;
                this.player2Units = player2Units;
            }
        }

        public override void OnPreEndSession()
        {
            board.DespawnCells();
            DespawnPlayerUnits(player1Units);
            DespawnPlayerUnits(player2Units);
        }

        private void DespawnPlayerUnits(UnitState[] playerUnits) 
        {
            for (int i = 0; i < playerUnits.Length; i++)
            {
                playerUnits[i].DespawnUnit();
            }
        }
    }
}
