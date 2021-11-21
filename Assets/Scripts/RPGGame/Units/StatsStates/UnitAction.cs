using RPGGame.GameDatasMan.Stats;
using System;

namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Current state of a Unit action and the cost of perform it
    /// In more complex games we can have debuffs and power-ups applied to this action
    /// </summary>
    public abstract class UnitAction : StatState, IUnitAction
    {
        public int BaseCost { get; private set; }

        private event Action onPerformEvent;

        public UnitAction(ActionData actionData, float level) : base(actionData, level)
        {
            BaseCost = actionData.Cost;
        }

        #region Listeners methods

        public void AddPerformListener(Action callback)
        {
            onPerformEvent += callback;
        }

        public void RemovePerformListener(Action callback)
        {
            onPerformEvent -= callback;
        }

        public override void RemoveAllListeners()
        {
            base.RemoveAllListeners();
            onPerformEvent = null;
        }

        #endregion

        public void Perform()
        {
            onPerformEvent?.Invoke();
        }
    }
}