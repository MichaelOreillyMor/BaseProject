using GFF.RegProviders;

using System;
using UnityEngine;

namespace GFF
{
    public abstract class BaseGameManager : MonoBehaviour
    {
        /// <summary>
        /// Initialization of the datas needed for this manager that don't have any dependency.
        /// </summary>
        /// <param name="onNextSetup">Next manager to load</param>
        public abstract void Setup(ISetProvidersRegister reg, Action onNextSetup);

        public abstract void Unsetup();
    }
}
