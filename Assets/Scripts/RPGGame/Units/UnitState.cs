using UnityEngine;

using GFFramework.Pools;
using GFFramework.Utils;

using RPGGame.GameDatas.Stats;
using RPGGame.Units.Stats;
using System;

namespace RPGGame.Units
{
    public class UnitState : PoolMember
    {
        public Vector2Int BoardPosition { get; private set; }
        public bool IsTeam1 { get; private set; }

        [SerializeField, ReadOnly]
        private UnitCosmeticController cosmetic;
        private UnitStatsState unitStatsState;

        public void SetInitSate(bool isTeam1, float level, UnitStatsData statsData, UnitCosmeticController unitCosmetic)
        {
            IsTeam1 = isTeam1;
            cosmetic = unitCosmetic;
            unitStatsState = new UnitStatsState(level, statsData);
        }

        public void SetBoardPosition(Vector2Int boardPosition)
        {
            BoardPosition = boardPosition;
        }

        public void ApplyAttackDamage(int damage)
        {
            cosmetic.PlayHit();
            unitStatsState.ApplyAttackDamage(damage);
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
    }
}