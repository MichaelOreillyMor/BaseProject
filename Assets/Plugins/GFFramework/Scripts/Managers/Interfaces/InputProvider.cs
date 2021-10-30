using GFFramework.Input;

namespace GFFramework
{
    public interface IInputProvider
    {
        public void SetIdlebacks(GameControls.IIdleStateActions idleStateActions);
        public void RemoveIdlebacks();
        public void SetUIbacks(GameControls.IUIStateActions uiStateActions);
        public void RemoveUIbacks();
    }
}