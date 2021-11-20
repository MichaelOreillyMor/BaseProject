using GFFramework;
using GFFramework.GameStates;

using RPGGame.BoardCells;
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

            ICameraProvider camProv = reg.CameraProv;
            IPoolProvider  poolProv = reg.PoolProv;

            unitsBoardFactory.Init(poolProv, camProv, UIProv);
            mapCellsFactory.Init(poolProv);
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
            MapLevelData map = dataProv.GetCurrentMapLevel();
            Board board = mapCellsFactory.CreateBoard(map.BoardSize);

            List<UnitState> Player1Units = unitsBoardFactory.CreateBoardUnits(map.Player1Units, board, true);
            List<UnitState> Player2Units = unitsBoardFactory.CreateBoardUnits(map.Player2Units, board, false);

            sessionProv.InitSession(board, Player1Units, Player2Units);
        }

        #endregion
    }
}