using GFFramework.Input;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFFramework.UI
{
    public abstract class BaseUIScreen : MonoBehaviour, IRequireInit, GameControls.IUIStateActions
    {
        public bool IsInit { get; private set; }

        public virtual void Setup()
        {
            IsInit = true;
        }

        public virtual void Unetup()
        {
            IsInit = false;
        }

        public virtual void OnBack(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("OnBack");
        }
    }
}