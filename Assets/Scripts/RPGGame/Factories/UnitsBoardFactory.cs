using GFFramework.Pools;
using RPGGame.BoardCells;
using RPGGame.GameDatas;
using UnityEngine;

namespace RPGGame.Units
{
    [System.Serializable]
    public class UnitsBoardFactory : PoolFactory
    {
        [SerializeField]
        private UnitState unitStatePref;

        public UnitState[] CreateBoardUnits(MapUnitData[] playerMapUnits, Board board, bool isTeam1)
        {
            int numUnits = playerMapUnits.Length;
            UnitState[] PlayerUnits = new UnitState[numUnits];

            for (int i = 0; i < numUnits; i++)
            {
                MapUnitData mapUnitData = playerMapUnits[i];
                UnitState unitState = CreateUnit(mapUnitData, isTeam1);

                board.AddUnit(unitState, mapUnitData.Position);
                PlayerUnits[i] = unitState;
            }

            return PlayerUnits;
        }

        private UnitState CreateUnit(MapUnitData mapUnitData, bool isTeam1)
        {
            UnitData unitData = mapUnitData.UnitData;
            UnitState unitState = PoolManager.Spawn(unitStatePref, Vector3.zero, Quaternion.identity);

            UnitCosmeticController unitCosmetic = PoolManager.Spawn(unitData.CosmeticPref, Vector3.zero, Quaternion.identity);
            unitCosmetic.transform.SetParent(unitState.transform);
            unitCosmetic.transform.localPosition = Vector3.zero;

            unitState.SetInitSate(isTeam1, mapUnitData.UnitLevel, unitData.UnitStats, unitCosmetic);
            return unitState;
        }
    }
}