using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// Base class for scriptableObjects that need an initialization (setup()) and an deinitialization (Unsetup()) of resources.
    /// It's useful basic base class for objects that don't need any parameter in the Setup() method.
    /// I use an abstract OnSetup() instead of making Setup() virtual to avoid the vtable and repeat the IsInit code
    /// </summary>
    public abstract class BaseInitScriptObj : ScriptableObject, IRequireInit
    {
        public bool IsInit { get; private set; }

        protected abstract void OnSetup();
        protected abstract void OnUnsetup();
        protected abstract void OnUpdate();

        public void Setup()
        {
            OnSetup();
            IsInit = true;
        }

        public void Unsetup()
        {
            if (IsInit)
            {
                OnUnsetup();
                IsInit = false;
            }
        }

        public void Update()
        {
            if (IsInit)
            {
                OnUpdate();
            }
        }
    }
}