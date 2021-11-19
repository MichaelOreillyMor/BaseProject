﻿using RPGGame.GameDatas.Stats;
using System;

namespace RPGGame.Units.Stats
{
    public class UnitAction : StatState, IUnitAction
    {
        public int Cost { get; private set; }

        private event Action onPerformEvent;

        public UnitAction(ActionData actionData, float level) : base(actionData, level)
        {
            Cost = actionData.Cost;
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