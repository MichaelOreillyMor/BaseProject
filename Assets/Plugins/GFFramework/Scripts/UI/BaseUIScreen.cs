using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// Base class for UIScreens (Canvas + UI objects) that need an initialization (setup()) and an deinitialization (Unsetup()) of resources.
    /// </summary>
    public abstract class BaseUIScreen : MonoBehaviour
    {
        /// <summary>
        /// Important to unregister the screen from any event that started to listen in the Setup()
        /// </summary>
        public abstract void Unsetup();
    }
}