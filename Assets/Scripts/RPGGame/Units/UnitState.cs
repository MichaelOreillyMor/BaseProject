using GFFramework.Pools;
using GFFramework.Utils;

using RPGGame.GameDatas.Stats;
using RPGGame.Units.Stats;

using UnityEngine;

namespace RPGGame.Units
{
    public class UnitState : PoolMember
    {
        public bool IsTeam1 { get; private set; }

        [SerializeField, ReadOnly]
        private UnitCosmetic cosmetic;

        private UnitStatsState unitStatsState;

        public void SetInitSate(bool isTeam1, float level, UnitStatsData statsData, UnitCosmetic unitCosmetic)
        {
            IsTeam1 = isTeam1;
            cosmetic = unitCosmetic;

            unitStatsState = new UnitStatsState(level, statsData);
        }

        public bool ApplyAttackDamage(int damage)
        {
            cosmetic.PlayHit();
            bool isDead = unitStatsState.ApplyAttackDamage(damage);

            return isDead;
        }

        public bool TryAttackUnit(UnitState otherUnit, int distance)
        {
            if (IsTeam1 != otherUnit.IsTeam1 )
            {
                if (unitStatsState.TryAttack(distance))
                {
                    int damame = unitStatsState.GetAttackDamage();
                    unitStatsState.ApplyAttackDamage(damame);
                    cosmetic.PlayAttack();
                    return true;
                }
            }

            return false;
        }

        public bool TryMovePosition(int distance)
        {
            if (unitStatsState.TryMove(distance))
            {
                cosmetic.PlayMove();
                return true;
            }

            return false;
        }

        public IUnitStatsState GetStatsState()
        {
            return unitStatsState;
        }

        public void DespawnUnit() 
        {
            unitStatsState.ForceDefeated();

            unitStatsState.RemoveAllListeners();
            cosmetic.Despawn();
            DespawnToPool();
        }
    }
}