using System;

namespace GFF.SessionsMan.TurnBasedSessions
{
    /// <summary>
    /// Acts as connexion point between the players and the GameController
    /// </summary>
    public class TurnBasedSessionManager : GameSessionManager, ITurnBasedSessionProvider
    {
        private ITurnBasedGameController gameController;

        #region Setup / Unsetup methods

        public void InitSession(ITurnBasedGameController gameController) 
        {
            if (TryInitSession())
            {
                this.gameController = gameController;
                gameController.StartGame();
                ResumeGame();
            }
        }

        public override void OnPreEndSession()
        {
            gameController.EndGame();
        }

        #endregion

        #region Turns methods

        public bool StartTurn(bool isPlayer1, Action onWinGameCallback)
        {
            if (AreTurnsEnabled())
            {
                return gameController.StartTurn(isPlayer1, onWinGameCallback);
            }
            else 
            {
                return false;
            }
        }

        public bool EndTurn(bool isPlayer1)
        {
            if (AreTurnsEnabled())
            {
                return gameController.EndTurn(isPlayer1);
            }
            else
            {
                return false;
            }
        }

        public bool AreTurnsEnabled() 
        {
            return (SessionStarted && !GamePaused);
        }

        #endregion
    }
}
