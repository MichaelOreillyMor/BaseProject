using GFFramework.UI;

namespace GFFramework
{
    public interface IUIProvider
    {
        public T LoadScreen<T>(BaseUIScreen screenPref) where T : BaseUIScreen;
        public void UnloadScreen();
        public T LoadHUD<T>() where T : BaseUIScreen;
        public void UnloadHUD();
        public BaseUIScreen ShowHUD(bool b);
    }
}