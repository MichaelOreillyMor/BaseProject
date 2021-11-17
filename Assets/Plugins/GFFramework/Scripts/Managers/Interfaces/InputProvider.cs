using GFFramework.Input;
using System;
using UnityEngine;

namespace GFFramework
{
    public interface IInputProvider
    {
        public void SetUICallbacks(GameControls.IUIScreenActions uiStateActions);
        public void RemoveUICallbacks();

        public void SetSelectWorldPointCalback(Action<Vector3> onHitPoint);
        public void RemovetWorldPointCalback();
    }
}