using GFF.ServiceLocators;

using System;
using UnityEngine;

namespace GFF.CamerasMan
{
    /// <summary>
    ///WIP, this will controll Cinemachine and the MainCamera
    /// </summary>
    public class CameraManager : BaseGameManager, ICameraManager
    {
        [SerializeField]
        private Camera gameCamera;

        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetup)
        {
            SetService(serviceLocator);
            onNextSetup?.Invoke();
        }

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<ICameraManager>(this);
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup CameraManager");
        }

        #endregion

        public Camera GetMainCamera() => gameCamera;
    }
}
