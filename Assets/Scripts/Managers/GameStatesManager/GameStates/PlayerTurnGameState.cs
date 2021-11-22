using GFF.Enums;
using GFF.SessionsMan.TurnBasedSessions;
using GFF.GameStatesMan.GameStates;
using GFF.InputsMan;
using GFF.RegProviders;
using GFF.UIsMan.UIScreens;

using RPGGame.UIsMan.HUD;

using UnityEngine;

namespace RPGGame.GameStatesMan.GameStates
{
    /// <summary>
    /// Handles the Local Player turn.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/PlayerTurnGameState")]
    public class PlayerTurnGameState : BaseUIGameState
    {
        private UIScreenHUD HUDScreen;

        private IInputProvider inputProv;
        private ITurnBasedSessionProvider sessionProv;

        #region Setup/Unsetup methods

        protected override void SetUIStateProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            sessionProv = (ITurnBasedSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is UIScreenHUD screen)
            {
                HUDScreen = screen;
                HUDScreen.Setup(OnEndTurn, OnSurrender);
                inputProv.EnableSelection();

                sessionProv.StartTurn(true, OnWinGame);
            }
            else
            {
                Debug.LogError(name + ": No valid UISreen to load", this);
            }
        }

        protected override void OnPreUIUnsetup()
        {
            inputProv.DisableSelection();
        }

        #endregion

        #region Callbacks methods

        private void OnEndTurn()
        {
            if (sessionProv.EndTurn(true))
            {
                LoadNextGameState();
            }
        }

        private void OnWinGame()
        {
            EndGame(GameStateKey.WinMenu);
        }

        private void OnSurrender()
        {
            EndGame(GameStateKey.LoseMenu);
        }

        private void OnReturnMainMenu()
        {
            EndGame(GameStateKey.LoadMainMenuScene);
        }

        private void EndGame(GameStateKey nextGameState)
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(nextGameState);
        }

        #endregion

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            OnReturnMainMenu();
            return true;
        }
    }
}