using GFFramework;
using GFFramework.GameStates;
using GFFramework.UI;
using RPGGame.Board;
using RPGGame.GameDatas;
using RPGGame.Units;

using System.Collections.Generic;
using UnityEngine;

namespace RPGGame.GameStates
{
    /// <summary>
    /// This the state that handles the load of a new a new GameSession.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSessionState")]
    public class LoadSessionGameState : BaseGameState
    {
        private IRPGDataProvider dataProv;
        private IRPGGameSessionProvider sessionProv;
        private IUIProvider UIProv;

        [SerializeField]
        private UnitStatesFactory unitStatesFactory;

        [SerializeField]
        private MapBoardFactory mapCellsFactory;

        private LoadScreen loadScreen;

        #region Setup/Unsetup methods

        protected override void OnSetProviders(IGetProvidersRegister reg)
        {
            dataProv = (IRPGDataProvider)reg.DataProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
            UIProv = reg.UIProv;

            unitStatesFactory.SetPool(reg.PoolProv);
            mapCellsFactory.SetPool(reg.PoolProv);
        }

        protected override void OnPostSetup()
        {
            loadScreen = UIProv.ShowLoadScreen(true);
            loadScreen.Setup();

            LoadGameSession();
            LoadNextGameState();
        }

        protected override void OnPreUnsetup()
        {
            loadScreen.Unsetup();
            UIProv.ShowLoadScreen(false);
        }

        #endregion

        public override void Update()
        {

        }

        #region Load Session methods

        private void LoadGameSession()
        {
            MapLevelData map = dataProv.GetCurrentMapLevel();

            List<UnitState> p1Units = unitStatesFactory.CreatePlayerUnits(map.Player1Units, true);
            List<UnitState> p2Units = unitStatesFactory.CreatePlayerUnits(map.Player2Units, false);

            MapBoard mapBoard = mapCellsFactory.CreateMapBoard(map.MapSize);
            mapBoard.AddInitUnits(p1Units);
            mapBoard.AddInitUnits(p2Units);
        }

        #endregion
    }
}