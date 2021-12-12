using System;

namespace GFF.SessionsMan.TurnBasedSessions
{
    public interface ITurnBasedSessionManager : IGameSessionManager
    {
        public void InitSession(ITurnBasedGameController gameController);
        public bool StartTurn(bool isPlayer1, Action onWinGameCallback);
        public bool EndTurn(bool isPlayer1);
    }
}