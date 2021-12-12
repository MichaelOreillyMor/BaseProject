using GFF.GameStatesMan.GameStates;
using GFF.Enums;
using GFF.SessionsMan.TurnBasedSessions;
using GFF.ServiceLocators;
using GFF.SessionsMan;

using UnityEngine;

namespace RPGGame.GameStatesMan.GameStates
{
    /// <summary>
    /// Handles an AI turn.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/AITurnGameState")]
    public class AITurnGameState : BaseGameState
    {
        private ITurnBasedSessionManager sessionMan;
        private bool hasWin;

        #region Setup/Unsetup methods

        protected override void OnSetServices(IGetService serviceLocator)
        {
            sessionMan = serviceLocator.GetService<ITurnBasedSessionManager>();
        }

        protected override void OnPostSetup()
        {
            PlayTurn();
        }

        protected override void OnPreUnsetup()
        {

        }

        #endregion

        #region AI methods

        private void PlayTurn()
        {
            hasWin = false;
            sessionMan.StartTurn(false, OnWinGame);

            if (!hasWin)
            {
                if (sessionMan.EndTurn(false))
                {
                    LoadNextGameState();
                }
            }
        }

        #endregion

        #region Callbacks methods

        private void OnWinGame()
        {
            hasWin = true;
            EndGame(GameStateKey.LoseMenu);
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
    }
}