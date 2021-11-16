using System;
using UnityEngine;

namespace GFFramework.GameSession
{
    /// <summary>
    ///Handles the current player session (e:g: the state of the match and the win state
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
