using GFFramework.Input;

using System;
using UnityEngine;

namespace GFFramework
{
    public class InputManager : BaseGameManager, IInputProvider
    {
        #region IGameManager

        private GameControls gameControls;

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SetInput(this);

            gameControls = new GameControls();
            gameControls.Enable();

            Debug.Log("Setup InputManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup InputManager");
        }

        public void SetIdlebacks(GameControls.IIdleStateActions idleStateActions)
        {
            gameControls.IdleState.SetCallbacks(idleStateActions);
        }

        public void RemoveIdlebacks()
        {
            gameControls.IdleState.SetCallbacks(null);
        }

        public void SetUIbacks(GameControls.IUIStateActions uiStateActions)
        {
            gameControls.UIState.SetCallbacks(uiStateActions);
        }

        public void RemoveUIbacks()
        {
            gameControls.UIState.SetCallbacks(null);
        }

        #endregion
    }
}