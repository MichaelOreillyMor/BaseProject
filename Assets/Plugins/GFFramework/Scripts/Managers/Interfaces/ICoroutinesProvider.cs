﻿using System;
using System.Collections;

namespace GFFramework
{
    public interface ICoroutinesProvider
    {
        public void StartGameStateCoroutine(IEnumerator coroutine);
        public IEnumerator StartDelayGameStateAction(Action actionCallback, int delay);
        public void StopGameStateCoroutine(IEnumerator coroutine);
        public void StopAllGameStateCoroutines();
    }
}