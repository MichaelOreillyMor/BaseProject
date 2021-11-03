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
        protected IInputProvider inputProv;

        protected sealed override void OnSetup()
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

        protected sealed override void OnUnsetup()
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

        protected abstract void OnPostUILoaded(BaseUIScreen uiScreen);
        protected abstract void OnPreUIUnsetup();

        public virtual void OnBack(InputAction.CallbackContext context)
        {
            if (canReturnPrevState)
            {
                LoadPrevGameState();
            }
        }
    }
}
