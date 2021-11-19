using GFFramework.UI;

using UnityEngine;

namespace GFFramework
{
    public interface IUIProvider
    {
        public BaseUIScreen LoadUIScreen(BaseUIScreen screenPref);
        public void UnloadUIScreen();
        public void RegisterSceneScreens(BaseUIScreen[] UIScreens);
        public void CleanSceneScreens();
        public void ShowLoadPanel();
        public void HideLoadPanel();
        public void AddContent(Transform panelTr);
    }
}