using System;

namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Manides to the UI (or any other listeners) only methods that don´t change the stats state
    /// </summary>
    public interface IReadUnitStatsState
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
