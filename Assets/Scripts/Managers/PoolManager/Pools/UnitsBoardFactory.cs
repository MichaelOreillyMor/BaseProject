using GFF.CamerasMan;
using GFF.PoolsMan;
using GFF.PoolsMan.Pools;
using GFF.UIsMan;

using RPGGame.BoardCells;
using RPGGame.DatasMan.GameDatas;
using RPGGame.UIsMan.StatsPanels;
using RPGGame.Units;
using RPGGame.Units.Stats;

using System.Collections.Generic;
using UnityEngine;

namespace RPGGame.PoolsMan.Pools
{
    [System.Serializable]
    public class UnitsBoardFactory : SpawnFactory
    {
        [SerializeField]
        private UnitState unitStatePref;

        [SerializeField]
        private UIPanelUnitStats panelUnitPref;

        private IUIManager UIMan;
        private Camera mainCamera;

        public void Init(ISpawnManager poolManager, ICameraManager cameraMan, IUIManager uiMan)
        {
            mainCamera = cameraMan.GetMainCamera();
            UIMan = uiMan;

            SetSpawner(poolManager);
        }

        public List<IUnitState> CreateBoardUnits(MapUnitData[] playerMapUnits, Board board, bool isTeam1)
        {
            int numUnits = playerMapUnits.Length;
            List<IUnitState> PlayerUnits = new List<IUnitState>(numUnits);

            for (int i = 0; i < numUnits; i++)
            {
                MapUnitData mapUnitData = playerMapUnits[i];
                UnitState unitState = CreateUnitState(mapUnitData, isTeam1);

                board.AddUnit(unitState, mapUnitData.Position);
                PlayerUnits.Add(unitState);
            }

            return PlayerUnits;
        }

        private UnitState CreateUnitState(MapUnitData mapUnitData, bool isTeam1)
        {
            UnitData unitData = mapUnitData.UnitData;
            UnitStatsState statsState = new UnitStatsState(mapUnitData.UnitLevel, unitData.UnitStats);

            UnitState unitState = Spawn(unitStatePref, Vector3.zero, Quaternion.identity);

            UnitCosmetic unitCosmetic = CreateUnitCosmetic(unitData, unitState);

            unitState.Setup(isTeam1, statsState, unitCosmetic);
            CreateUnitStatsPanel(statsState, unitCosmetic, isTeam1);

            return unitState;
        }

        private UnitCosmetic CreateUnitCosmetic(UnitData unitData, UnitState unitState)
        {
            UnitCosmetic unitCosmetic = Spawn(unitData.CosmeticPref, Vector3.zero, Quaternion.identity);
            unitCosmetic.transform.SetParent(unitState.transform);
            unitCosmetic.transform.localPosition = Vector3.zero;

            unitCosmetic.Setup(mainCamera.transform);
            return unitCosmetic;
        }

        private void CreateUnitStatsPanel(IReadUnitStatsState statsState, UnitCosmetic unitCosmetic, bool isTeam1)
        {
            UIPanelUnitStats panelUnitStats = Spawn(panelUnitPref, Vector3.zero, Quaternion.identity);
            UIMan.AddContent(panelUnitStats.transform);

            Transform anchorPoint = unitCosmetic.GetPanelAnchor();

            panelUnitStats.Setup(statsState, anchorPoint, isTeam1, mainCamera);
        }
    }
}