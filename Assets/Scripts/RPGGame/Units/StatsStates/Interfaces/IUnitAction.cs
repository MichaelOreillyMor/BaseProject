using System;

namespace RPGGame.Units.Stats
{
    public interface IUnitAction : IStatState
    {
        public int Cost { get; }

        public void AddPerformListener(Action callback);
        public void RemovePerformListener(Action callback);
    }
}