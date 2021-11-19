using System;
using UnityEngine;

namespace GFFramework.Input
{
    /// <summary>
    /// Basic class to encapsulate the inputs maps and control the input listeners
    /// </summary>
    public class InputManager : BaseGameManager, IInputProvider
    {
        [SerializeField]
        private LayerMask selectWorldLayer;

        private GameControls gameControls;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetupCallback)
        {
            reg.InputProv = this;

            gameControls = new GameControls();
            gameControls.Enable();

            Debug.Log("Setup InputManager");
            onNextSetupCallback?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup InputManager");
        }

        #endregion

        public void SetUICallbacks(GameControls.IUIScreenActions uiStateActions)
        {
            gameControls.UIScreen.SetCallbacks(uiStateActions);
        }

        public void RemoveUICallbacks()
        {
            gameControls.UIScreen.SetCallbacks(null);
        }

        public void SetSelectWorldPointCalback(Action<Vector3> onHitPoint)
        {
   
        }

        public void RemovetWorldPointCalback()
        {

        }
    }
}