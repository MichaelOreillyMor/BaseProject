using System;

namespace GFF.SessionsMan.TurnBasedSessions
{
    public interface ITurnBasedGameController
    {
        public void StartGame();
        public void EndGame();
        public bool StartTurn(bool isPlayer1, Action onWinGameCallback);
        public bool EndTurn(bool isPlayer1);
    }
}
