using RPGGame.UI;

using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates.UI;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace RPGGame.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/PlayerTurnGameState")]
    public class PlayerTurnGameState : BaseUIGameState
    {
        private HUDScreen HUDScreen;

        private IInputProvider inputProv;
        private  IRPGGameSessionProvider sessionProv;

        #region Setup/Unsetup methods

        protected override void SetUIProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is HUDScreen screen)
            {
                HUDScreen = screen;
                HUDScreen.Setup();
            }
            else
            {
                Debug.LogError(name + ": No valid UISreen to load", this);
            }
        }

        protected override void OnPreUIUnsetup()
        {

        }

        #endregion

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            gameStateProv.LoadGameState(GameStateKey.LoadMainMenuScene);
            return true;
        }

        public void OnSelectMap(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            Debug.Log("input " + input);
        }
    }
}