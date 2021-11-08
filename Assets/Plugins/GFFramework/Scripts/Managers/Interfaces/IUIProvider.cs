using GFFramework.UI;

namespace GFFramework
{
    public interface IUIProvider
    {
        public BaseUIScreen LoadUIScreen(BaseUIScreen screenPref);
        public void UnloadUIScreen();
        public void RegisterSceneScreens(BaseUIScreen[] UIScreens);
        public void CleanSceneScreens();
        public LoadScreen ShowLoadScreen(bool show);
    }
}