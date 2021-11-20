using GFFramework;
using GFFramework.Pools;

using RPGGame.BoardCells;
using RPGGame.GameDatas;
using RPGGame.UI.HUD;
using RPGGame.Units.Stats;

using System.Collections.Generic;
using UnityEngine;

namespace RPGGame.Units
{
    [System.Serializable]
    public class UnitsBoardFactory : SpawnFactory
    {
        [SerializeField]
        private UnitState unitStatePref;

        [SerializeField]
        private UIPanelUnitStats panelUnitPref;

        private IUIProvider UIProvider;
        private Camera mainCamera;

        public void Init(ISpawnProvider poolManager, ICameraProvider cameraProv, IUIProvider uiProvider)
        {
            mainCamera = cameraProv.GetMainCamera();
            UIProvider = uiProvider;

            SetSpawner(poolManager);
        }

        public List<UnitState> CreateBoardUnits(MapUnitData[] playerMapUnits, Board board, bool isTeam1)
        {
            int numUnits = playerMapUnits.Length;
            List<UnitState> PlayerUnits = new List<UnitState>(numUnits);

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
            UnitState unitState = Spawn(unitStatePref, Vector3.zero, Quaternion.identity);

            UnitCosmetic unitCosmetic = CreateUnitCosmetic(unitData, unitState);
            unitState.Setup(isTeam1, mapUnitData.UnitLevel, unitData.UnitStats, unitCosmetic);
            CreateUnitStatsPanel(unitState, unitCosmetic, isTeam1);

            return unitState;
        }

        private UnitCosmetic CreateUnitCosmetic(UnitData unitData, UnitState unitState)
        {
            UnitCosmetic unitCosmetic = Spawn(unitData.CosmeticPref, Vector3.zero, Quaternion.identity);
            unitCosmetic.transform.SetParent(unitState.transform);
            unitCosmetic.transform.localPosition = Vector3.zero;

            unitCosmetic.Init(mainCamera.transform);
            return unitCosmetic;
        }

        private void CreateUnitStatsPanel(UnitState unitState, UnitCosmetic unitCosmetic, bool isTeam1)
        {
            UIPanelUnitStats panelUnitStats = Spawn(panelUnitPref, Vector3.zero, Quaternion.identity);
            UIProvider.AddContent(panelUnitStats.transform);

            IUnitStatsState unitStats = unitState.GetStatsState();
            Transform anchorPoint = unitCosmetic.GetPanelAnchor();

            panelUnitStats.Setup(unitStats, anchorPoint, isTeam1, mainCamera);
        }
    }
}