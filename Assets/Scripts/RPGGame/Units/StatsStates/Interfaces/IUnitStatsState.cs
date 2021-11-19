using System;

namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Provides to the UI (or any other listeners) just the information that they need. No methods that change the stats values
    /// </summary>
    public interface IUnitStatsState
    {
        public float Level { get; }
        public IStatState GetActionPoints();
        public IStatState GetHealth();
        public IUnitAction GetMoveAction();
        public IUnitActionRange GetAttackAction();

        public void AddDefeatedListener(Action callback);
        public void RemoveDefeatedListener(Action callback);
    }
}
