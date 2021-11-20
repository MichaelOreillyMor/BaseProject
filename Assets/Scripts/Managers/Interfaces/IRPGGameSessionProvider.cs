using GFFramework;

using RPGGame.BoardCells;
using RPGGame.Units;

using System;
using System.Collections.Generic;

namespace RPGGame
{
    public interface IRPGGameSessionProvider : IGameSessionProvider
    {
        public void InitSession(Board board, List<UnitState> player1Units, List<UnitState> player2Units);
        public void StartTurn(Action onWinGameCallback);
        public bool EndTurn(bool isPlayer1);
        public void PlayAI();
    }
}