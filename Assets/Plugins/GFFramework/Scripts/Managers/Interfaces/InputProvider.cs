using GFFramework.Inputs;
using System;
using UnityEngine;

namespace GFFramework
{
    public interface IInputProvider
    {
        public void SetUICallbacks(GameControls.IUIScreenActions uiStateActions);
        public void RemoveUICallbacks();
        public void SetSelectWorldObjectCalback(Action<Transform> onSelectCallback);
        public void RemoveSelectWorldObjectCalback();
    }
}