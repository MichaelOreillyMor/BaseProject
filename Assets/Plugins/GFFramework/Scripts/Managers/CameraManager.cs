using System;
using UnityEngine;

namespace GFFramework.Cameras
{
    /// <summary>
    ///WIP, this will controll Cinemachine and the MainCamera
    /// </summary>
    public class CameraManager : BaseGameManager, ICameraProvider
    {
        [SerializeField]
        private Camera gameCamera;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.CameraProv = this;

            Debug.Log("Setup CameraManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup CameraManager");
        }

        #endregion

        public Camera GetMainCamera() => gameCamera;
    }
}
