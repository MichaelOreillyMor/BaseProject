using GFF.PoolsMan.Pools;
using GFF.Utils;

using RPGGame.GameDatasMan.Stats;
using RPGGame.Units.Stats;

using System;
using UnityEngine;

namespace RPGGame.Units
{
    /// <summary>
    /// Current state of a Unit (e.g: Soldier, Monster), it acts as a 
    /// facade (pattern) to connect the Cosmetic, Transform and Stats (life, attack, actionPoints...)
    /// </summary>
    public class UnitState : PoolMember, IUnitState
    {
        public bool IsTeam1 { get; private set; }

        [SerializeField, ReadOnly]
        private UnitCosmetic cosmetic;

        private IWriteUnitStatsState statsState;

        #region Setup methods

        public void Setup(bool isTeam1, IWriteUnitStatsState statsState, UnitCosmetic unitCosmetic)
        {
            IsTeam1 = isTeam1;
            cosmetic = unitCosmetic;
            this.statsState = statsState;
        }

        #endregion

        #region Unsetup methods

        public void Unsetup()
        {
            statsState.ForceDefeated();

            statsState.RemoveAllListeners();
            cosmetic.Unsetup();
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

        public bool TryAttackUnit(IUnitState otherUnit, int distance, Action<IUnitState> onDeadCallback)
        {
            if (otherUnit != null && IsTeam1 != otherUnit.IsTeam1)
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

        #region Move methods

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

        #endregion

        #region Other methods

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        #endregion
    }
}