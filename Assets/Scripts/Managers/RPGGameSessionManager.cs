using GFFramework.GameSession;
using RPGGame.BoardCells;
using RPGGame.Units;

namespace RPGGame.GameSession
{
    public class RPGGameSessionManager : GameSessionManager, IRPGGameSessionProvider
    {
        protected bool GameStarted { get; private set; }

        public void InitSession(Board board, UnitState[] player1Units, UnitState[] player2Units) 
        {
            if (!GameStarted)
            {
                GameStarted = true;
            }
        }

        public override void EndSession()
        {
            if (GameStarted)
            {
                GameStarted = false;
            }
        }
    }
}
