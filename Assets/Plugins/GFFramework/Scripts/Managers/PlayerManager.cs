using System;
using UnityEngine;

namespace GFFramework.PlayerControllers
{
    /// <summary>
    /// Basic class to keep a reference to the BasePlayerController
    /// </summary>
    public class PlayerManager : BaseGameManager, IPlayerProvider
    {
        private BasePlayerCharacter scenePlayerCharacter;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.PlayerProv = this;

            Debug.Log("Setup PlayerManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup PlayerManager");
        }


        #endregion

        public void RegisterScenePlayerCharacter(BasePlayerCharacter playerCharacter) => scenePlayerCharacter = playerCharacter;
        public void CleanPlayerCharacter() => scenePlayerCharacter = null;

        public BasePlayerCharacter GetPlayerCharacter() => scenePlayerCharacter;
    }
}
