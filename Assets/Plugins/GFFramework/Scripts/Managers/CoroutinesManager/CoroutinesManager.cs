using GFF.RegProviders;

using System;
using System.Collections;
using UnityEngine;

namespace GFF.CoroutinesMan
{
    /// <summary>
    /// WIP Handles the current GameState coroutines, it´s safely to StopAllCoroutines() in Unsetup()
    /// I have to check if StopAllCoroutines() generates garbaje or cosumes CPU resources if we don´t have any coroutine running.
    /// </summary>
    public class CoroutinesManager : BaseGameManager, ICoroutinesProvider
    {
        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetup)
        {
            SetService(serviceLocator);

            Debug.Log("Setup CoroutinesManager");
            onNextSetup?.Invoke();
        }

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<ICoroutinesProvider>(this);
        }

        public override void Unsetup()
        {
            Debug.Log("Unsetup CoroutinesManager");
        }

        #endregion

        #region Coroutines helpers

        public void StartGameStateCoroutine(IEnumerator coroutine)
        {
            if (coroutine != null)
            {
                StartCoroutine(coroutine);
            }
        }

        public void StopGameStateCoroutine(IEnumerator coroutine)
        {
            if (coroutine != null)
            {
                StartCoroutine(coroutine);
            }
        }

        public IEnumerator StartDelayGameStateAction(Action action, int delay)
        {
            IEnumerator delayCoroutine = null;
            if (action != null)
            {
                delayCoroutine = DelayAction(action, delay);
                StartCoroutine(delayCoroutine);
            }

            return delayCoroutine;
        }

        private IEnumerator DelayAction(Action action, int delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }

        public void StopAllGameStateCoroutines()
        {
            StopAllCoroutines();
        }

        #endregion
    }
}
