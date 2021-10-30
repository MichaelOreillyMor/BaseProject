using GFFramework;
using GFFramework.Input;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [CreateAssetMenu(menuName = "GameStates/IdleGameState")]
    public class IdleGameState : BaseGameState, GameControls.IIdleStateActions
    {
        PlayerController playerController;

        public override void Setup()
        {
            uiProv.ShowHUD(true);
            playerController = (PlayerController)playerProv.GetPlayerController();

            inputProv.SetIdlebacks(this);
        }

        public override void Unsetup()
        {
            inputProv.RemoveIdlebacks();
            uiProv.ShowHUD(false);
        }

        public override void Update()
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