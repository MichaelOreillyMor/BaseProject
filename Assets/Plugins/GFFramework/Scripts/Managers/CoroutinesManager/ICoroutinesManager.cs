using System;
using System.Collections;

namespace GFF.CoroutinesMan
{
    public interface ICoroutinesManager
    {
        public void StartGameStateCoroutine(IEnumerator coroutine);
        public IEnumerator StartDelayGameStateAction(Action actionCallback, int delay);
        public void StopGameStateCoroutine(IEnumerator coroutine);
        public void StopAllGameStateCoroutines();
    }
}