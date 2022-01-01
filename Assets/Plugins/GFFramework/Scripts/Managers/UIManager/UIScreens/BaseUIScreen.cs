using UnityEngine;

namespace GFF.UIsMan.UIScreens
{
    /// <summary>
    /// Base class for UIScreens (Canvas + UI components) that need an initialization (setup()) and an deinitialization (Unsetup()) of resources.
    /// It´s not an Interface to be able to reference them in the scene hierarchy.
    /// </summary>
    public abstract class BaseUIScreen : MonoBehaviour
    {
        /// <summary>
        /// Important to unregister the screen from any event that started to listen in the Setup()
        /// </summary>
        public abstract void Unsetup();

        public void Show(bool show) => gameObject.SetActive(show);
    }
}