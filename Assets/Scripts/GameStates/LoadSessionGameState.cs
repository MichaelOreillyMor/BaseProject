using GFFramework;
using GFFramework.GameStates;
using GFFramework.UI;

using RPGGame.BoardCells;
using RPGGame.GameDatas;
using RPGGame.Units;

using System;
using UnityEngine;

namespace RPGGame.GameStates
{
    /// <summary>
    /// This the state that handles the load of a new a new GameSession.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSessionState")]
    public class LoadSessionGameState : BaseGameState
    {
        private IRPGGameSessionProvider sessionProv;
        private IRPGDataProvider dataProv;
        private IUIProvider UIProv;

        [SerializeField]
        private UnitsBoardFactory unitsBoardFactory;

        [SerializeField]
        private MapBoardFactory mapCellsFactory;

        #region Setup/Unsetup methods

        protected override void OnSetProviders(IGetProvidersRegister reg)
        {
            dataProv = (IRPGDataProvider)reg.DataProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
            UIProv = reg.UIProv;

            unitsBoardFactory.SetPool(reg.PoolProv);
            mapCellsFactory.SetPool(reg.PoolProv);
        }

        protected override void OnPostSetup()
        {
            UIProv.ShowLoadPanel();

            LoadGameSession();
            LoadNextGameState();
        }

        protected override void OnPreUnsetup()
        {
            UIProv.HideLoadPanel();
        }

        #endregion

        public override void Update()
        {

        }

        #region Load Session methods

        private void LoadGameSession()
        {
            Action<Transform> onCreatePanelCallback = UIProv.AddContent;

            MapLevelData map = dataProv.GetCurrentMapLevel();
            Board board = mapCellsFactory.CreateBoard(map.BoardSize);

            UnitState[] Player1Units = unitsBoardFactory.CreateBoardUnits(map.Player1Units, board, true, onCreatePanelCallback);
            UnitState[] Player2Units = unitsBoardFactory.CreateBoardUnits(map.Player2Units, board, false, onCreatePanelCallback);

            sessionProv.InitSession(board, Player1Units, Player2Units);
        }

        #endregion
    }
}