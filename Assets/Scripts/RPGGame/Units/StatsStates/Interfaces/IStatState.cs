using System;

namespace RPGGame.Units.Stats
{
    public interface IStatState
    {
        public int InitValue { get; }
        public int Value { get; }
        public void AddValueChangeListener(Action<int, int> callback);
        public void RemoveValueChangeListener(Action<int, int> callback);
    }
}