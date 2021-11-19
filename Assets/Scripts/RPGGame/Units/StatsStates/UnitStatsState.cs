using RPGGame.GameDatas.Stats;

using System;

namespace RPGGame.Units.Stats
{
    class UnitStatsState : IUnitStatsState
    {
        public float Level { get; private set; }

        private StatState actionPoints;
        private StatState health;

        private UnitAttack attack;
        private UnitMovement move;

        private event Action onDefeatedEvent;

        public UnitStatsState(float level, UnitStatsData unitStats)
        {
            Level = level;

            actionPoints = new StatState(unitStats.ActionPoints, Level);
            health = new StatState(unitStats.Health, Level);
            attack = new UnitAttack(unitStats.Attack, Level);
            move = new UnitMovement(unitStats.Move, Level);
        }

        public void ForceDefeated()
        {
            ApplyAttackDamage(health.Value);
        }

        #region Listeners methods

        public void AddDefeatedListener(Action callback) 
        {
            onDefeatedEvent += callback;
        }

        public void RemoveDefeatedListener(Action callback)
        {
            onDefeatedEvent -= callback;
        }

        public void RemoveAllListeners()
        {
            actionPoints.RemoveAllListeners();
            health.RemoveAllListeners();
            attack.RemoveAllListeners();
            move.RemoveAllListeners();
            onDefeatedEvent = null;
        }

        #endregion

        #region Attack methods

        public bool ApplyAttackDamage(int damage)
        {
            health.SubtractValue(damage);
            bool isDead = (health.Value <= 0);

            if (isDead)
                onDefeatedEvent?.Invoke();

            return isDead;
        }

        public bool TryAttack(int distance)
        {
            if (attack.HasCost(actionPoints.Value) && attack.HasRange(distance))
            {
                actionPoints.SubtractValue(attack.Cost);
                attack.Perform();
                return true;
            }

            return false;
        }

        public int GetAttackDamage()
        {
            return attack.Value;
        }

        #endregion

        #region Move methods

        public bool TryMove(int distance)
        {
            if (move.HasCost(distance, actionPoints.Value))
            {
                actionPoints.SubtractValue(move.Cost);
                move.Perform();
                return true;
            }

            return false;
        }

        #endregion

        #region Points methods

        public void ResetActionPoints()
        {
            actionPoints.ResetValue();
        }

        #endregion

        #region Get stats methods

        public IStatState GetActionPoints()
        {
            return actionPoints;
        }

        public IStatState GetHealth()
        {
            return health;
        }

        public IUnitAction GetMoveAction()
        {
            return move;
        }

        public IUnitActionRange GetAttackAction()
        {
            return attack;
        }

        #endregion
    }
}
