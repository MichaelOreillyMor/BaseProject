using GFF.ServiceLocators;

using System;
using UnityEngine;

namespace GFF
{
    /// <summary>
    /// This is not an Interface in order to drag and drop them in the editor
    /// </summary>
    public abstract class BaseGameManager : MonoBehaviour
    {
        /// <summary>
        /// Initialization of the datas needed for this manager that don't have any dependency.
        /// </summary>
        /// <param name="onNextSetup">Next manager to load</param>
        public abstract void Setup(ISetService serviceLocator, Action onNextSetup);

        protected abstract void SetService(ISetService serviceLocator);

        public abstract void Unsetup();
    }
}
