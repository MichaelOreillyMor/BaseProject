using GFFramework.Input;

namespace GFFramework
{
    public interface IInputProvider
    {
        public void SetIdleCallbacks(GameControls.IIdleStateActions idleStateActions);
        public void RemoveIdleCallbacks();
        public void SetUICallbacks(GameControls.IUIStateActions uiStateActions);
        public void RemoveUICallbacks();
    }
}