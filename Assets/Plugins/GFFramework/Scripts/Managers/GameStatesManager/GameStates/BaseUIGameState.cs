using GFF.InputsMan;
using GFF.InputsMan.InputActions;
using GFF.RegProviders;
using GFF.UIsMan;
using GFF.UIsMan.UIScreens;

using UnityEngine;
using UnityEngine.InputSystem;

namespace GFF.GameStatesMan.GameStates
{
    public abstract class BaseUIGameState : BaseGameState, GameControls.IUIScreenActions
    {
        //WIP I don't like to have three references to the same UIScreen (2 + upcasted one in the derived class)
        [SerializeField]
        private BaseUIScreen uiScreenPref;
        private BaseUIScreen uiScreen;

        [SerializeField]
        protected bool canReturnPrevState;

        protected IUIProvider uiProv;
        private IInputProvider inputProv;

        #region Setup/Unsetup methods

        protected sealed override void OnSetProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;

            SetUIStateProviders(reg);
        }

        /// <summary>
        /// Here any derived UIGameState gets the references to the providers that needs.
        /// </summary>
        /// 
        protected abstract void SetUIStateProviders(IGetProvidersRegister reg);

        protected sealed override void OnPostSetup()
        {
            if (uiScreenPref)
            {
                uiScreen = uiProv.LoadUIScreen(uiScreenPref);
                inputProv.SetUICallbacks(this);
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
                Debug.Log(uiScreen.name + " Unsetup");

                inputProv.RemoveUICallbacks();
                uiScreen.Unsetup();
                uiProv.UnloadUIScreen();
            }
        }

        /// <summary>
        /// The UIScreen should be setup here
        /// </summary>
        protected abstract void OnPostUILoaded(BaseUIScreen uiScreen);

        /// <summary>
        /// Unsetup what you need here before UIScreen Unsetup()
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

        public void OnSelect(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
