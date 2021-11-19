using GFFramework.Pools;

using RPGGame.BoardCells;
using RPGGame.GameDatas;
using RPGGame.UI.HUD;
using RPGGame.Units.Stats;
using System;
using UnityEngine;

namespace RPGGame.Units
{
    [System.Serializable]
    public class UnitsBoardFactory : PoolFactory
    {
        [SerializeField]
        private UnitState unitStatePref;

        [SerializeField]
        private UIPanelUnitStats panelUnitPref;

        Action<Transform> onCreatePanelCallback;

        public UnitState[] CreateBoardUnits(MapUnitData[] playerMapUnits, Board board, bool isTeam1, 
            Action<Transform> onCreatePanelCallback)
        {
            this.onCreatePanelCallback = onCreatePanelCallback;

            int numUnits = playerMapUnits.Length;
            UnitState[] PlayerUnits = new UnitState[numUnits];

            for (int i = 0; i < numUnits; i++)
            {
                MapUnitData mapUnitData = playerMapUnits[i];
                UnitState unitState = CreateUnitState(mapUnitData, isTeam1);

                board.AddUnit(unitState, mapUnitData.Position);
                PlayerUnits[i] = unitState;
            }

            return PlayerUnits;
        }

        private UnitState CreateUnitState(MapUnitData mapUnitData, bool isTeam1)
        {
            UnitData unitData = mapUnitData.UnitData;
            UnitState unitState = PoolManager.Spawn(unitStatePref, Vector3.zero, Quaternion.identity);

            UnitCosmetic unitCosmetic = CreateUnitCosmetic(unitData, unitState);
            unitState.SetInitSate(isTeam1, mapUnitData.UnitLevel, unitData.UnitStats, unitCosmetic);
            CreateUnitStatsPanel(unitState, unitCosmetic, isTeam1);

            return unitState;
        }

        private UnitCosmetic CreateUnitCosmetic(UnitData unitData, UnitState unitState)
        {
            UnitCosmetic unitCosmetic = PoolManager.Spawn(unitData.CosmeticPref, Vector3.zero, Quaternion.identity);
            unitCosmetic.transform.SetParent(unitState.transform);
            unitCosmetic.transform.localPosition = Vector3.zero;
            return unitCosmetic;
        }

        private void CreateUnitStatsPanel(UnitState unitState, UnitCosmetic unitCosmetic, bool isTeam1)
        {
            UIPanelUnitStats panelUnitStats = PoolManager.Spawn(panelUnitPref, Vector3.zero, Quaternion.identity);
            onCreatePanelCallback(panelUnitStats.transform);

            IUnitStatsState unitStats = unitState.GetStatsState();
            Transform anchorPoint = unitCosmetic.GetPanelAnchor();

            panelUnitStats.Init(unitStats, anchorPoint, isTeam1);
        }
    }
}