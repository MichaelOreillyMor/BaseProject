using System;

namespace GFF.SessionsMan.TurnBasedSessions
{
    public interface ITurnBasedSessionProvider : IGameSessionProvider
    {
        public void InitSession(ITurnBasedGameController gameController);
        public bool StartTurn(bool isPlayer1, Action onWinGameCallback);
        public bool EndTurn(bool isPlayer1);
    }
}