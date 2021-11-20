using RPGGame.UI.HUD;

using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates.UI;
using GFFramework.UI;

using UnityEngine;
using RPGGame.BoardCells;

namespace RPGGame.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/PlayerTurnGameState")]
    public class PlayerTurnGameState : BaseUIGameState
    {
        private UIScreenHUD HUDScreen;

        private IInputProvider inputProv;
        private IRPGGameSessionProvider sessionProv;

        #region Setup/Unsetup methods

        protected override void SetUIStateProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is UIScreenHUD screen)
            {
                HUDScreen = screen;
                HUDScreen.Setup(OnEndTurn, OnSurrender);
                inputProv.SetSelectWorldObjectCalback(OnCellSelected);

                sessionProv.StartTurn(OnWinGame);
            }
            else
            {
                Debug.LogError(name + ": No valid UISreen to load", this);
            }
        }

        protected override void OnPreUIUnsetup()
        {
            inputProv.RemoveSelectWorldObjectCalback();
        }

        #endregion

        private void OnEndTurn()
        {
            if (sessionProv.EndTurn(true))
            {
                LoadNextGameState();
            }
        }

        private void OnWinGame()
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.WinMenu);
        }

        private void OnSurrender()
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.LoseMenu);
        }

        private void OnCellSelected(Transform obj) 
        {
            //WIP: I will try to remove the GetComponent if I have time enough.
            Cell cell =  obj.GetComponent<Cell>();
            cell?.Select();
        }

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            OnCloseSession();
            return true;
        }

        private void OnCloseSession()
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.LoadMainMenuScene);
        }
    }
}