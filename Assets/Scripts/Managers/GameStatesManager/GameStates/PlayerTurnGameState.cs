using GFF.SessionsMan.TurnBasedSessions;
using GFF.GameStatesMan.GameStates;
using GFF.GameStatesMan.Keys;
using GFF.InputsMan;
using GFF.ServiceLocators;
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

        private IInputManager inputMan;
        private ITurnBasedSessionManager sessionMan;

        #region Setup/Unsetup methods

        protected override void SetUIStateServices(IGetService serviceLocator)
        {
            inputMan = serviceLocator.GetService<IInputManager>();
            sessionMan = serviceLocator.GetService<ITurnBasedSessionManager>();
        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is UIScreenHUD screen)
            {
                HUDScreen = screen;
                HUDScreen.Setup(OnEndTurn, OnSurrender);
                inputMan.EnableSelection();

                sessionMan.StartTurn(true, OnWinGame);
            }
            else
            {
                Debug.LogError(name + ": No valid UISreen to load", this);
            }
        }

        protected override void OnPreUIUnsetup()
        {
            inputMan.DisableSelection();
        }

        #endregion

        #region Callbacks methods

        private void OnEndTurn()
        {
            if (sessionMan.EndTurn(true))
            {
                LoadNextGameState();
            }
        }

        private void OnWinGame()
        {
            EndGame(GameStateKey.WinGameState);
        }

        private void OnSurrender()
        {
            EndGame(GameStateKey.LoseGameState);
        }

        private void OnReturnMainMenu()
        {
            EndGame(GameStateKey.LoadSceneMainMenu);
        }

        private void EndGame(GameStateKey nextGameState)
        {
            sessionMan.EndSession();
            gameStateMan.LoadGameState(nextGameState);
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