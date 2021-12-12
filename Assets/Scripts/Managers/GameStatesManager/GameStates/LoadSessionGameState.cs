using GFF.CamerasMan;
using GFF.SessionsMan.TurnBasedSessions;
using GFF.UIsMan;
using GFF.GameStatesMan.GameStates;
using GFF.ServiceLocators;
using GFF.PoolsMan;

using RPGGame.BoardCells;
using RPGGame.DatasMan;
using RPGGame.SessionsMan;
using RPGGame.Units;
using RPGGame.DatasMan.GameDatas;
using RPGGame.PoolsMan.Pools;
using RPGGame.SessionsMan.Players;
using GFF.DatasMan;
using GFF.SessionsMan;

using RPGGame.SessionsMan.Players.AIs;

using System.Collections.Generic;
using UnityEngine;


namespace RPGGame.GameStatesMan.GameStates
{
    /// <summary>
    /// Handles the load of a new a new GameSession
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadSessionState")]
    public class LoadSessionGameState : BaseGameState
    {
        private ITurnBasedSessionManager sessionMan;
        private IRPGDataManager dataMan;
        private IUIManager UIMan;

        [SerializeField]
        private UnitsBoardFactory unitsBoardFactory;

        [SerializeField]
        private MapBoardFactory mapCellsFactory;

        #region Setup/Unsetup methods

        protected override void OnSetServices(IGetService serviceLocator)
        {
            dataMan = serviceLocator.GetService<IRPGDataManager>();
            sessionMan = serviceLocator.GetService<ITurnBasedSessionManager>();
            UIMan = serviceLocator.GetService<IUIManager>();

            ICameraManager camMan = serviceLocator.GetService<ICameraManager>();
            IPoolManager  poolMan = serviceLocator.GetService<IPoolManager>();

            unitsBoardFactory.Init(poolMan, camMan, UIMan);
            mapCellsFactory.Init(poolMan);
        }

        protected override void OnPostSetup()
        {
            UIMan.ShowLoadPanel();

            LoadPlayerVsAI();
            LoadNextGameState();
        }

        protected override void OnPreUnsetup()
        {
            UIMan.HideLoadPanel();
        }

        #endregion

        #region Load Session methods

        private void LoadPlayerVsAI()
        {
            //We get the current game level that the player is going to play
            MapLevelData map = dataMan.GetCurrentMapLevel();
            //Then we create:

            //The Board and its Cells
            Board board = mapCellsFactory.CreateBoard(map.BoardSize);

            //Player 1 controlled by local Input
            List<IUnitState> player1Units = unitsBoardFactory.CreateBoardUnits(map.Player1Units, board, true);
            PlayerRPG player1 = new PlayerRPG(player1Units, null);

            //Player 2 controlled by an AI
            List<IUnitState> player2Units = unitsBoardFactory.CreateBoardUnits(map.Player2Units, board, false);
            IAController IAController = new IAController(board, player2Units.Count, player1Units.Count, false);
            PlayerRPG player2 = new PlayerRPG(player2Units, IAController);

            //GameController and callbacks needed
            GameRPGController gameController = new GameRPGController(board, player1, player2);
            board.SetOnSelectCallback(gameController.OnSelectCell);
            IAController.SetOnSelectCallback(gameController.OnSelectCell);

            //The game session can start now
            sessionMan.InitSession(gameController);
        }

        #endregion

        public override void Update()
        {

        }
    }
}