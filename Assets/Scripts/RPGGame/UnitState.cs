using UnityEngine;

using GFFramework.Pools;

namespace RPGGame.Units
{
    public class UnitState : PoolMember
    {
        [SerializeField]
        UnitCosmeticController cosmetic;
    }
}