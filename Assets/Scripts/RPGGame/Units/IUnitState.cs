using RPGGame.Units.Stats;

using System;
using UnityEngine;

namespace RPGGame.Units
{
    public interface IUnitState
    {
        public bool IsTeam1 { get; }

        public bool ApplyAttackDamage(int damage);
        public void ResetActionPoints();
        public void Setup(bool isTeam1, IWriteUnitStatsState statsState, UnitCosmetic unitCosmetic);
        public bool TryAttackUnit(IUnitState otherUnit, int distance, Action<IUnitState> onDeadCallback);
        public bool TryMovePosition(int distance);
        public void SetWorldPosition(Vector3 position);
        public void Unsetup();
    }
}