using System;

namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Provides to the UI (or any other listeners) just the information that they need. No methods that change the stats values
    /// </summary>
    interface IUnitStatsState
    {
        public float Level { get; }
        public IStatState ActionPoints { get; }
        public IStatState Health { get; }
        public IUnitAction Move { get; }
        public IUnitActionRange Attack { get; }

        public void AddDefeatedListener(Action callback);
        public void RemoveDefeatedListener(Action callback);
    }
}
