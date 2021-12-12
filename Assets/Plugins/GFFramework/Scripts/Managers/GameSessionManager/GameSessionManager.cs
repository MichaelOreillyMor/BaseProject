using GFF.RegProviders;

using System;
using UnityEngine;

namespace GFF.SessionsMan
{
    /// <summary>
    ///Handles the current play session
    /// </summary>
    public abstract class GameSessionManager : BaseGameManager, IGameSessionProvider
    {
        protected bool SessionStarted { get; private set; }
        protected bool GamePaused { get; private set; }

        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetupCallback)
        {
            SetService(serviceLocator);

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
            if (GamePaused)
            {
                GamePaused = false;
            }
        }

        public void StopGame()
        {
            if (!GamePaused)
            {
                GamePaused = true;
            }
        }

        protected bool TryInitSession()
        {
            if (!SessionStarted)
            {
                SessionStarted = true;
                return true;
            }

            return false;
        }

        public void EndSession()
        {
            if (SessionStarted)
            {
                OnPreEndSession();
                SessionStarted = false;
            }
        }

        public abstract void OnPreEndSession();
    }
}
