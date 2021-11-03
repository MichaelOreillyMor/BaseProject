using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// Base class for objects that need an initialization (setup()) and an deinitialization (Unsetup()) of resources.
    /// It's useful basic base class for objects that don't need any parameter in the Setup() method.
    /// I use an abstract OnSetup() instead of making Setup() virtual to avoid the vtable and repeat the IsInit code
    /// </summary>
    public abstract class BaseInitMonoObj : MonoBehaviour, IRequireInit
    {
        public bool IsInit => isInit;
        private bool isInit;

        protected abstract void OnSetup();
        protected abstract void OnUnsetup();
        protected abstract void OnUpdate();

        public void Setup()
        {
            OnSetup();
            isInit = true;
        }

        public void Unsetup()
        {
            if (isInit)
            {
                Unsetup();
                isInit = false;
            }
        }

        private void Update()
        {
            if (IsInit)
            {
                OnUpdate();
            }
        }
    }
}