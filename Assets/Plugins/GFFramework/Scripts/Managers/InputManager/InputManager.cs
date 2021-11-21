using GFF.InputsMan.InputActions;
using GFF.RegProviders;

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFF.InputsMan
{
    /// <summary>
    /// Basic class to encapsulate the inputs maps and control the input listeners
    /// </summary>
    public class InputManager : BaseGameManager, IInputProvider
    {
        [SerializeField]
        private LayerMask selectWorldLayer;

        private GameControls gameControls;

        private bool canSelectObjs;
        private RaycastHit hit;

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

        public void EnableSelection()
        {
            canSelectObjs = true;
        }

        public void DisableSelection()
        {
            canSelectObjs = false;
        }

        private void Update()
        {
            if (canSelectObjs)
            {
                CheckSelectWorldObject();
            }
        }

        private void CheckSelectWorldObject()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out hit, 100.0f, selectWorldLayer))
                {
                    ISelectable selectable = hit.transform.GetComponent<ISelectable>();
                    selectable?.Select();
                }
            }
        }
    }
}