using GFF.GameStatesMan.GameStates;
using GFF.Enums;
using GFF.SessionsMan.TurnBasedSessions;
using GFF.RegProviders;
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
        private ITurnBasedSessionProvider sessionProv;
        private bool hasWin;

        #region Setup/Unsetup methods

        protected override void OnSetProviders(IGetService serviceLocator)
        {
            sessionProv = serviceLocator.GetService<ITurnBasedSessionProvider>();
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
            sessionProv.StartTurn(false, OnWinGame);

            if (!hasWin)
            {
                if (sessionProv.EndTurn(false))
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
            sessionProv.EndSession();
            gameStateProv.LoadGameState(nextGameState);
        }

        #endregion 

        public override void Update()
        {

        }
    }
}