using GFF.InputsMan;
using GFF.InputsMan.InputActions;
using GFF.ServiceLocators;
using GFF.UIsMan;
using GFF.UIsMan.UIScreens;

using UnityEngine;
using UnityEngine.InputSystem;

namespace GFF.GameStatesMan.GameStates
{
    /// <summary>
    /// Base class that gets a UIScreen from the IUIManager and shows it to the player. 
    /// </summary>
    public abstract class BaseUIGameState : BaseGameState, GameControls.IUIScreenActions
    {
        //WIP I don't like to have three references to the same UIScreen (2 + upcasted one in the derived class)
        [SerializeField]
        private BaseUIScreen uiScreenPref;
        private BaseUIScreen uiScreen;

        [SerializeField]
        protected bool canReturnPrevState;

        protected IUIManager uiMan;
        private IInputManager inputMan;

        #region Setup/Unsetup methods

        protected sealed override void OnSetServices(IGetService serviceLocator)
        {
            uiMan = serviceLocator.GetService<IUIManager>();
            inputMan = serviceLocator.GetService<IInputManager>();

            SetUIStateServices(serviceLocator);
        }

        /// <summary>
        /// Here any derived UIGameState gets the references to the providers that needs.
        /// </summary>
        /// 
        protected abstract void SetUIStateServices(IGetService serviceLocator);

        protected sealed override void OnPostSetup()
        {
            if (uiScreenPref)
            {
                uiScreen = uiMan.LoadUIScreen(uiScreenPref);
                inputMan.SetUICallbacks(this);
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

                inputMan.RemoveUICallbacks();
                uiScreen.Unsetup();
                uiMan.UnloadUIScreen();
            }
        }

        /// <summary>
        /// Here you should call to UIScreen.Setup() and resolve the dependencies of any other GameState component.
        /// </summary>
        protected abstract void OnPostUILoaded(BaseUIScreen uiScreen);

        /// <summary>
        /// Unsetup what you need here. The UIScreen.Unsetup() will be called after this.
        /// </summary>
        protected abstract void OnPreUIUnsetup();

        #endregion

        #region navigation methods

        /// <summary>
        /// Returns true if the Goback InputAction was captured by the screen (e.g: a UI panel was closed)
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
    }
}
