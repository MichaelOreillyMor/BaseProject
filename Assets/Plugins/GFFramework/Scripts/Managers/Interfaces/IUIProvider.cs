using GFFramework.UI;

namespace GFFramework
{
    public interface IUIProvider
    {
        public BaseUIScreen LoadScreen(BaseUIScreen screenPref);
        public void UnloadScreen();
        public T LoadHUD<T>() where T : BaseUIScreen;
        public void UnloadHUD();
        public BaseUIScreen ShowHUD(bool b);
    }
}