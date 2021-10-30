﻿using System;
using UnityEngine;

namespace GFFramework
{
    public class SceneManager : BaseGameManager, ISceneProvider
    {
        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SetScene(this);
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
