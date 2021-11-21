using RPGGame.BoardCells;
using System;

namespace RPGGame.SessionsMan.AI
{
    public interface IPlayerAI
    {
        void Play();
        void SetOnSelectCallback(Action<ICell> onSelectCell);
    }
}