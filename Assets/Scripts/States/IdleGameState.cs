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

        protected override void OnSetup()
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            playerProv = reg.PlayerProv;

            uiProv.ShowHUD(true);
            playerController = (PlayerController)playerProv.GetPlayerController();
            inputProv.SetIdleCallbacks(this);
        }

        protected override void OnUnsetup()
        {
            inputProv.RemoveIdleCallbacks();
            uiProv.ShowHUD(false);
        }

        protected override void OnUpdate()
        {
  
        }

        public void OnMainAction(InputAction.CallbackContext context)
        {
            Debug.Log("OnMainShot " + context.ReadValue<float>());
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("OnMove " + context.ReadValue<Vector2>());
        }
    }
}