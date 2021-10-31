using GFFramework.PlayerControlles;

using System;
using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// Basic class to keep a reference to the BasePlayerController
    /// </summary>
    public class PlayerManager : BaseGameManager, IPlayerProvider
    {
        [SerializeField]
        private BasePlayerController playerControllerPref;
        private BasePlayerController playerController;

        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SetPlayer(this);
            Debug.Log("Setup PlayerManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            UnloadPlayerController();
            Debug.Log("Unsetup PlayerManager");
        }


        #endregion

        public void LoadPlayerController()
        {
            if (playerControllerPref)
            {
                if (this.playerController)
                {
                    UnloadPlayerController();
                }

                this.playerController = Instantiate(playerControllerPref, Vector3.zero, Quaternion.identity);
            }
        }

        public BasePlayerController GetPlayerController()
        {
            return playerController;
        }

        public void UnloadPlayerController()
        {
            if (playerController)
            {
                playerController.Unetup();
                Destroy(playerController.gameObject);
            }
        }
    }
}
