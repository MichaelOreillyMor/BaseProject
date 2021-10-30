using System;
using UnityEngine;

namespace GFFramework
{
    public class PoolManager : BaseGameManager, IPoolProvider
    {
        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.SetPool(this);
            Debug.Log("Setup PoolManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup PoolManager");
        }

        #endregion
    }
}