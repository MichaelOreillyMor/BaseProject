using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates;

using UnityEngine;

namespace RPGGame.GameStates
{
    /// <summary>
    /// This the base state that handles the load of a new scene and the dispose of the previous one.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/EnemyTurnGameState")]
    public class EnemyTurnGameState : BaseGameState
    {
        private IRPGGameSessionProvider sessionProv;
        private bool hasWin;

        #region Setup/Unsetup methods

        protected override void OnSetProviders(IGetProvidersRegister reg)
        {
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostSetup()
        {
            PlayTurn();
        }

        protected override void OnPreUnsetup()
        {

        }

        #endregion

        private void PlayTurn()
        {
            hasWin = false;
            sessionProv.StartTurn(OnWinGame);
            sessionProv.PlayAI();

            if (!hasWin)
            {
                if (sessionProv.EndTurn(false))
                {
                    LoadNextGameState();
                }
            }
        }

        private void OnWinGame()
        {
            hasWin = true;
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.LoseMenu);
        }

        public override void Update()
        {

        }
    }
}