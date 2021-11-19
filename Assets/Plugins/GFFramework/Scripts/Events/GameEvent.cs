using GFFramework.Utils;

using System;
using UnityEngine;

namespace GFFramework.Events
{
    /// <summary>
    /// This is a WIP, not used yet
    /// </summary>
    /// 
    [System.Serializable]
    public class GameEvent : IGameEvent
    {
        [SerializeField, ReadOnly]
        private int numListeners;

        private event Action gameEvent;

        public GameEvent()
        {
            numListeners = 0;
            gameEvent = null;
        }

        public void AddListener(Action callback)
        {
            numListeners++;
            gameEvent += callback;
        }

        public void Removeistener(Action callback)
        {
            numListeners--;
            gameEvent -= callback;
        }

        public void Invoke()
        {
            gameEvent?.Invoke();
        }

        public void RemoveAllListeners()
        {
            gameEvent = null;
        }
    }
}