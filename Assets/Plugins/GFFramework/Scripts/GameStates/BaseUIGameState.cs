using GFFramework.Input;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace GFFramework.GameStates.UI
{
    public abstract class BaseUIGameState : BaseGameState, GameControls.IUIStateActions
    {
        //WIP I don't like to have three references to the same UIScreen (2 + upcasted one in the derived class)
        [SerializeField]
        private BaseUIScreen uiScreenPref;
        private BaseUIScreen uiScreen;

        [SerializeField]
        private bool isMenuInputListener;

        [SerializeField]
        protected bool canReturnPrevState;

        protected IUIProvider uiProv;
        private IInputProvider inputProv;

        #region Setup/Unsetup methods

        protected sealed override void SetProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;

            SetUIProviders(reg);
        }

        /// <summary>
        /// Method executed before Setup(), here any derived UIGameState gets the references to the providers that needs.
        /// </summary>
        /// 
        protected abstract void SetUIProviders(IGetProvidersRegister reg);

        protected sealed override void OnPostSetup()
        {
            if (uiScreenPref)
            {
                uiScreen = uiProv.LoadUIScreen(uiScreenPref);

                if (isMenuInputListener)
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

        protected sealed override void OnPreUnsetup()
        {
            OnPreUIUnsetup();

            if (uiScreen)
            {
                if (isMenuInputListener)
                {
                    inputProv.RemoveUICallbacks();
                }

                uiScreen.Unsetup();
                uiProv.UnloadUIScreen();
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

        #endregion

        #region navigation methods

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

        #endregion

        #region editor methods

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                SetKeyToUIScreen();
            }
#endif
        }

        /// <summary>
        /// Adds its key to the UIScreen as a link between them
        /// </summary>
        private void SetKeyToUIScreen()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                if (uiScreenPref)
                {
                    uiScreenPref.SetOwner(Key);
                }
            }
#endif
        }

        #endregion
    }
}
