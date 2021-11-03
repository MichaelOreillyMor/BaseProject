using System;
using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// Base class for UIScreens (Canvas + UI objects) that need an initialization (setup()) and an deinitialization (Unsetup()) of resources.
    /// It´s a variation of the BaseInitMonoObj class
    /// </summary>
    public abstract class BaseUIScreen : MonoBehaviour
    {
        public bool IsInit { get; private set; }

        protected void OnSetup()
        {
            IsInit = true;
        }

        public void Unsetup()
        {
            if (IsInit)
            {
                OnUnsetup();
            }
        }

        protected abstract void OnUnsetup();
    }
}