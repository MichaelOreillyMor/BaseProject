using GFF.InputsMan.InputActions;

namespace GFF.InputsMan
{
    public interface IInputManager
    {
        public void SetUICallbacks(GameControls.IUIScreenActions uiStateActions);
        public void RemoveUICallbacks();
        public void EnableSelection();
        public void DisableSelection();
    }
}