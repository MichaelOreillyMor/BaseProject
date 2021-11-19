using System;

namespace GFFramework.Events
{
    public interface IGameEvent
    {
        void AddListener(Action callback);
        void Removeistener(Action callback);
    }
}