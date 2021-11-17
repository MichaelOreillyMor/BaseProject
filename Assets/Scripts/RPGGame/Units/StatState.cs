using RPGGame.GameDatas.Stats;
using System;
using UnityEngine;

namespace RPGGame.Units.Stats
{
    [System.Serializable]
    public class StatState : IStatState
    {
        public int Value { get; private set; }
        public int InitValue { get; private set; }

        private event Action<int> onValueChange;

        public StatState(StatData statData, float level)
        {
            InitValue = Mathf.FloorToInt(statData.BaseValue + ((level - 1f) * statData.ScalePerLevel));
            Value = InitValue;
        }

        public void AddValueChangeListener(Action<int> callback) => onValueChange += callback;
        public void RemoveValueChangeListener(Action<int> callback) => onValueChange -= callback;

        public void AddValue(int value)
        {
            Value += value;

            if (Value < 0)
                Value = 0;

            onValueChange?.Invoke(Value);
        }

        public void ResetValue()
        {
            Value = InitValue;
            onValueChange?.Invoke(Value);
        }
    }
}
