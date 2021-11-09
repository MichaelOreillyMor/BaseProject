using Game.PlayerControlles;
using Game.UI;

using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates.UI;
using GFFramework.Input;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/IdleGameState")]
    public class IdleGameState : BaseUIGameState, GameControls.IIdleStateActions
    {
        private PlayerCharacter playerCharacter;
        private HUDScreen HUDScreen;

        private IInputProvider inputProv;
        private  IPlayerProvider playerProv;
        private IGameStateProvider GameStateProv;

        #region Setup/Unsetup methods

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is HUDScreen screen)
            {
                uiProv = Reg.UIProv;
                inputProv = Reg.InputProv;
                playerProv = Reg.PlayerProv;
                GameStateProv = Reg.GameStateProv;

                playerCharacter = (PlayerCharacter)playerProv.GetPlayerCharacter();
                playerCharacter.Setup();

                inputProv.SetIdleCallbacks(this);

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
            inputProv.RemoveIdleCallbacks();
            playerCharacter.Unsetup();
        }

        #endregion

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            GameStateProv.LoadGameState(GameStateKey.LoadMainMenuScene);
            return true;
        }

        public void OnMainAction(InputAction.CallbackContext context)
        {
            Debug.Log("OnMainAction " + context.ReadValue<float>());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            playerCharacter.SetDirection(input);
        }
    }
}