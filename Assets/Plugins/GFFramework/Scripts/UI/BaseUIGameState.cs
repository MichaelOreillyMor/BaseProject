using GFFramework.Input;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace GFFramework.GameStates.UI
{
    public abstract class BaseUIGameState : BaseGameState, GameControls.IUIStateActions
    {
        [SerializeField]
        private BaseUIScreen uiScreenPref;
        private BaseUIScreen uiScreen;

        [SerializeField]
        private bool isInputListener;

        [SerializeField]
        protected bool canReturnPrevState;

        protected IUIProvider uiProv;
        private IInputProvider inputProv;

        public sealed override void Setup()
        {
            if (uiScreenPref)
            {
                uiProv = reg.UIProv;
                inputProv = reg.InputProv;

                uiScreen = uiProv.LoadScreen(uiScreenPref);

                if (isInputListener)
                {
                    inputProv.SetUICallbacks(this);
                }

                OnPostUILoaded(uiScreen);
            }
            else 
            {
                Debug.LogError(name + ": No UISreen to load", this);
            }
        }

        public sealed override void Unsetup()
        {
            OnPreUIUnsetup();

            if (uiScreen)
            {
                if (isInputListener)
                {
                    inputProv.RemoveUICallbacks();
                }

                uiScreen.Unsetup();
                uiProv.UnloadScreen();
            }
        }

        /// <summary>
        /// The UIScreen should be setup here
        /// </summary>
        protected abstract void OnPostUILoaded(BaseUIScreen uiScreen);

        /// <summary>
        /// Unsetup what you need here before UIScreen unloading
        /// </summary>
        protected abstract void OnPreUIUnsetup();

        /// <summary>
        /// Returns true if back was captured by the screen (e.g: a UI panel was closed)
        /// </summary>
        public virtual bool OnBack() => false;

        public void OnBack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                bool backInputCaptured = OnBack();

                if (backInputCaptured && canReturnPrevState)
                {
                    LoadPrevGameState();
                }
            }
        }
    }
}
