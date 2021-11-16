using GFFramework.Input;

namespace GFFramework
{
    public interface IInputProvider
    {
        public void SetUICallbacks(GameControls.IUIScreenActions uiStateActions);
        public void RemoveUICallbacks();
    }
}