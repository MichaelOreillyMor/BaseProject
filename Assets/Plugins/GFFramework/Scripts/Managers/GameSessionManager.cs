using System;
using UnityEngine;

namespace GFFramework.GameSession
{
    /// <summary>
    ///Handles the current play session and the rules of the game
    /// </summary>
    public abstract class GameSessionManager : BaseGameManager, IGameSessionProvider
    {
        protected bool GameStarted { get; private set; }
        protected bool GamePaused { get; private set; }

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetupCallback)
        {
            reg.GameSessionProv = this;
            GamePaused = true;

            Debug.Log("Setup GameSessionManager");
            onNextSetupCallback?.Invoke();
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

        protected bool TryInitSession()
        {
            if (!GameStarted)
            {
                GameStarted = true;
                return true;
            }

            return false;
        }

        public void EndSession()
        {
            if (GameStarted)
            {
                OnPreEndSession();
                GameStarted = false;
            }
        }

        public abstract void OnPreEndSession();
    }
}
