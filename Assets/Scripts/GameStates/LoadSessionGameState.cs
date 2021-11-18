using GFFramework;
using GFFramework.GameStates;
using GFFramework.UI;

using RPGGame.BoardCells;
using RPGGame.GameDatas;
using RPGGame.Units;

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

        private LoadScreen loadScreen;

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
            Board board = mapCellsFactory.CreateBoard(map.BoardSize);

            UnitState[] Player1Units = unitsBoardFactory.CreateBoardUnits(map.Player1Units, board, true);
            UnitState[] Player2Units = unitsBoardFactory.CreateBoardUnits(map.Player2Units, board, false);

            sessionProv.InitSession(board, Player1Units, Player2Units);
        }

        #endregion
    }
}