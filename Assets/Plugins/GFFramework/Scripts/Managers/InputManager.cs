using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFFramework.Inputs
{
    /// <summary>
    /// Basic class to encapsulate the inputs maps and control the input listeners
    /// </summary>
    public class InputManager : BaseGameManager, IInputProvider
    {
        [SerializeField]
        private LayerMask selectWorldLayer;

        private GameControls gameControls;

        private Action<Transform> onSelectCallback;
        private bool isListeningSelect;
        private RaycastHit hit;
        private Ray ray;

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

        public void SetSelectWorldObjectCalback(Action<Transform> onSelectCallback)
        {
            isListeningSelect = true;
            this.onSelectCallback = onSelectCallback;
        }

        public void RemoveSelectWorldObjectCalback()
        {
            isListeningSelect = false;
            onSelectCallback = null;
        }

        private void Update()
        {
            if (isListeningSelect)
            {
                CheckSelectWorldObject();
            }
        }

        private void CheckSelectWorldObject()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out hit, 100.0f, selectWorldLayer))
                {
                    onSelectCallback?.Invoke(hit.transform);
                }
            }
        }
    }
}