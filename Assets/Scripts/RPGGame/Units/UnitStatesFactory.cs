using System.Collections.Generic;
using UnityEngine;

using GFFramework.Pools;

using RPGGame.GameDatas;

namespace RPGGame.Units
{
    [System.Serializable]
    public class UnitStatesFactory : PoolFactory
    {
        [SerializeField]
        private UnitState unitStatePref;

        public List<UnitState> CreatePlayerUnits(MapUnitData[] playerUnits, bool isTeam1)
        {
            int numUnits = playerUnits.Length;
            List<UnitState> unitStates = new List<UnitState>(numUnits);

            for (int i = 0; i < numUnits; i++)
            {
                MapUnitData mapUnitData = playerUnits[i];
                UnitState unitState = CreateUnit(mapUnitData, isTeam1);
                unitStates.Add(unitState);
            }

            return unitStates;
        }

        private UnitState CreateUnit(MapUnitData mapUnitData, bool isTeam1)
        {
            UnitData unitData = mapUnitData.UnitData;

            UnitState unitState = PoolManager.Spawn(unitStatePref, Vector3.zero, Quaternion.identity);

            UnitCosmeticController unitCosmetic = PoolManager.Spawn(unitData.CosmeticPref, Vector3.zero, Quaternion.identity);
            unitCosmetic.transform.SetParent(unitState.transform);
            unitCosmetic.transform.localPosition = Vector3.zero;

            unitState.SetInitSate(isTeam1, mapUnitData.UnitLevel, unitData.UnitStats, unitCosmetic);
            unitState.SetBoardPosition(mapUnitData.Position);

            return unitState;
        }
    }
}