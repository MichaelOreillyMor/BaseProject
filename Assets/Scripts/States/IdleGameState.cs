using Game.PlayerControlles;

using GFFramework;
using GFFramework.GameStates;
using GFFramework.Input;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/IdleGameState")]
    public class IdleGameState : BaseGameState, GameControls.IIdleStateActions
    {
        PlayerController playerController;

        protected IUIProvider uiProv;
        protected IInputProvider inputProv;
        protected IPlayerProvider playerProv;

        public override void Setup()
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            playerProv = reg.PlayerProv;

            uiProv.ShowHUD(true);
            playerController = (PlayerController)playerProv.GetPlayerController();
            inputProv.SetIdleCallbacks(this);
        }

        public override void Unsetup()
        {
            inputProv.RemoveIdleCallbacks();
            uiProv.ShowHUD(false);
        }

        public override void Update()
        {
  
        }

        public void OnMainAction(InputAction.CallbackContext context)
        {
            Debug.Log("OnMainAction " + context.ReadValue<float>());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            playerController.SetDirection(input);
        }
    }
}