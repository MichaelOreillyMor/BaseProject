using GFF.PoolsMan.Pools;
using GFF.Utils;

using RPGGame.GameDatasMan.Stats;
using RPGGame.Units.Stats;
using System;
using UnityEngine;

namespace RPGGame.Units
{
    public class UnitState : PoolMember
    {
        public bool IsTeam1 { get; private set; }

        [SerializeField, ReadOnly]
        private UnitCosmetic cosmetic;

        private UnitStatsState statsState;

        #region Setup methods

        public void Setup(bool isTeam1, float level, UnitStatsData statsData, UnitCosmetic unitCosmetic)
        {
            IsTeam1 = isTeam1;
            cosmetic = unitCosmetic;

            statsState = new UnitStatsState(level, statsData);
        }

        #endregion

        #region Unsetup methods

        public void Unsetup()
        {
            statsState.ForceDefeated();

            statsState.RemoveAllListeners();
            cosmetic.Despawn();
            DespawnToPool();
        }

        #endregion

        #region Attack methods

        public bool ApplyAttackDamage(int damage)
        {
            cosmetic.PlayHit();
            bool isDead = statsState.ApplyAttackDamage(damage);

            return isDead;
        }

        public bool TryAttackUnit(UnitState otherUnit, int distance, Action<UnitState> onDeadCallback)
        {
            if (otherUnit && IsTeam1 != otherUnit.IsTeam1)
            {
                if (statsState.TryAttack(distance))
                {
                    int damame = statsState.GetAttackDamage();
                    bool isDead = otherUnit.ApplyAttackDamage(damame);
                    cosmetic.PlayAttack();

                    if (isDead)
                    {
                        onDeadCallback?.Invoke(otherUnit);
                    }

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Stats methods

        public bool TryMovePosition(int distance)
        {
            if (statsState.TryMove(distance))
            {
                cosmetic.PlayMove();
                return true;
            }

            return false;
        }

        #endregion

        #region Stats methods

        public void ResetActionPoints()
        {
            statsState.ResetActionPoints();
        }

        public IUnitStatsState GetStatsState()
        {
            return statsState;
        }

        #endregion
    }
}