﻿using System;
using UnityEngine;

namespace GFFramework.Scenes
{
    /// <summary>
    /// TO-DO
    /// </summary>
    public class SceneManager : BaseGameManager, ISceneProvider
    {
        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SceneProv = this;

            Debug.Log("Setup SceneManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup SceneManager");
        }


        #endregion
    }
}
