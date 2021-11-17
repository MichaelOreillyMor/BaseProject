using RPGGame.GameDatas.Stats;

using System;
using UnityEngine;

namespace RPGGame.Units.Stats
{
    class UnitStatsState : IUnitStatsState
    {
        public float Level { get; private set; }
        public IStatState ActionPoints => actionPoints;
        public IStatState Health => health;
        public IUnitAction Move => move;
        public IUnitActionRange Attack => attack;

        private StatState actionPoints;
        private StatState health;

        private UnitAttack attack;
        private UnitMovement move;

        private event Action onDefeated;

        public UnitStatsState(float level, UnitStatsData unitStats)
        {
            Level = level;

            actionPoints = new StatState(unitStats.ActionPoints, Level);
            health = new StatState(unitStats.Health, Level);
            attack = new UnitAttack(unitStats.Attack, Level);
            move = new UnitMovement(unitStats.Move, Level);
        }

        public void AddDefeatedListener(Action callback) => onDefeated += callback;
        public void RemoveDefeatedListener(Action callback) => onDefeated -= callback;

        public void ApplyAttackDamage(int damage)
        {
            health.AddValue(-damage);

            if (health.Value == 0)
                onDefeated?.Invoke();
        }

        public bool TryAttack(int distance)
        {
            if (attack.HasCost(actionPoints.Value) && attack.HasRange(distance))
            {
                actionPoints.AddValue(attack.Cost);
                return true;
            }

            return false;
        }

        public int GetAttackDamage()
        {
            return attack.Value;
        }

        public bool TryMove(int distance)
        {
            if (move.HasCost(distance, actionPoints.Value))
            {
                actionPoints.AddValue(move.Cost);
                return true;
            }

            return false;
        }

        public void ResetActionPoints()
        {
            actionPoints.ResetValue();
        }
    }
}
