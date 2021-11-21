using RPGGame.GameDatasMan.Stats;
using System;
using UnityEngine;

namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Current state of a Unit stat, the initial value is loaded from a serialized data
    /// </summary>
    public class StatState : IStatState
    {
        public int Value { get; private set; }
        public int InitValue { get; private set; }

        private event Action<int, int> onValueChangeEvent;

        #region Listeners methods

        public StatState(StatData statData, float level)
        {
            InitValue = Mathf.FloorToInt(statData.BaseValue + ((level - 1f) * statData.ScalePerLevel));
            Value = InitValue;
        }

        public void AddValueChangeListener(Action<int, int> callback)
        {
            onValueChangeEvent += callback;
        }

        public void RemoveValueChangeListener(Action<int, int> callback)
        {
            onValueChangeEvent -= callback;
        }

        public virtual void RemoveAllListeners()
        {
            onValueChangeEvent = null;
        }

        #endregion

        #region Change value methods

        public void AddValue(int value)
        {
            Value += value;

            if (Value < 0)
                Value = 0;

            onValueChangeEvent?.Invoke(Value, InitValue);
        }

        public void SubtractValue(int value)
        {
            Value -= value;

            if (Value < 0)
                Value = 0;

            onValueChangeEvent?.Invoke(Value, InitValue);
        }

        public void ResetValue()
        {
            Value = InitValue;
            onValueChangeEvent?.Invoke(Value, InitValue);
        }

        #endregion
    }
}
