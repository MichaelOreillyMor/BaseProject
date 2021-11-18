using System;
using UnityEngine;

namespace GFFramework.GameSession
{
    /// <summary>
    ///Handles the current play session and the rules of the game
    /// </summary>
    public abstract class GameSessionManager : BaseGameManager, IGameSessionProvider
    {
        protected bool GamePaused { get; private set; }

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.GameSessionProv = this;
            GamePaused = true;

            Debug.Log("Setup GameSessionManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            EndSession();
            Debug.Log("Unsetup GameSessionManager");
        }

        #endregion

        public void ResumeGame()
        {
            if (!GamePaused)
            {
                GamePaused = true;
            }
        }

        public void StopGame()
        {
            if (GamePaused)
            {
                GamePaused = false;
            }
        }

        public abstract void EndSession();
    }
}
