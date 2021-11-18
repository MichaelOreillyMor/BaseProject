using RPGGame.GameDatas.Stats;
using System;

namespace RPGGame.Units.Stats
{
    public class UnitAction : StatState, IUnitAction
    {
        public int Cost { get; private set; }

        private event Action onPerform;

        public UnitAction(ActionData actionData, float level) : base(actionData, level)
        {
            Cost = actionData.Cost;
        }

        public void AddPerformListener(Action callback)
        {
            onPerform += callback;
        }

        public void RemovePerformListener(Action callback)
        {
            onPerform -= callback;
        }

        public void Perform()
        {
            onPerform?.Invoke();
        }
    }
}