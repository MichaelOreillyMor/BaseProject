using GFF.CamerasMan;
using GFF.SessionsMan.TurnBasedSessions;
using GFF.UIsMan;
using GFF.GameStatesMan.GameStates;
using GFF.RegProviders;
using GFF.PoolsMan;

using RPGGame.BoardCells;
using RPGGame.DatasMan;
using RPGGame.SessionsMan;
using RPGGame.Units;
using RPGGame.DatasMan.GameDatas;
using RPGGame.PoolsMan.Pools;
using RPGGame.SessionsMan.AI;

using System.Collections.Generic;
using UnityEngine;

namespace RPGGame.GameStatesMan.GameStates
{
    /// <summary>
    /// This the state that handles the load of a new a new GameSession.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSessionState")]
    public class LoadSessionGameState : BaseGameState
    {
        private ITurnBasedSessionProvider sessionProv;
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
            sessionProv = (ITurnBasedSessionProvider)reg.GameSessionProv;
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

            List<IUnitState> player1Units = unitsBoardFactory.CreateBoardUnits(map.Player1Units, board, true);
            List<IUnitState> player2Units = unitsBoardFactory.CreateBoardUnits(map.Player2Units, board, false);

            PlayerAI playerAI = new PlayerAI(board, player2Units.Count, player1Units.Count, false);

            PlayerVsAIController gameController = new PlayerVsAIController(board, playerAI, player1Units, player2Units);

            sessionProv.InitSession(gameController);
        }

        #endregion
    }
}