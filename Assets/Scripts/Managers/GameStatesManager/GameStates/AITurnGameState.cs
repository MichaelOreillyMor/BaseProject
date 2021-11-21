using GFF.GameStatesMan.GameStates;
using GFF.Enums;
using GFF.SessionsMan.TurnBasedSessions;
using GFF.RegProviders;

using UnityEngine;

namespace RPGGame.GameStatesMan.GameStates
{
    /// <summary>
    /// This the base state that handles the load of a new scene and the dispose of the previous one.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/AITurnGameState")]
    public class AITurnGameState : BaseGameState
    {
        private const bool isPlayer1 = false;

        private ITurnBasedSessionProvider sessionProv;
        private bool hasWin;

        #region Setup/Unsetup methods

        protected override void OnSetProviders(IGetProvidersRegister reg)
        {
            sessionProv = (ITurnBasedSessionProvider)reg.GameSessionProv;
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
            sessionProv.StartTurn(isPlayer1, OnWinGame);

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