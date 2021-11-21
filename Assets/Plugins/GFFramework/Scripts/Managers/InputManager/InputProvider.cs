using GFF.InputsMan.InputActions;
using System;
using UnityEngine;

namespace GFF.InputsMan
{
    public interface IInputProvider
    {
        public void SetUICallbacks(GameControls.IUIScreenActions uiStateActions);
        public void RemoveUICallbacks();
        public void EnableSelection();
        public void DisableSelection();
    }
}