using GFF.ScenesMan.Utils;
using GFF.UIsMan.UIScreens;

using UnityEngine;

namespace GFF.UIsMan
{
    public interface IUIManager
    {
        public BaseUIScreen LoadUIScreen(BaseUIScreen screenPref);
        public void UnloadUIScreen();
        public void RegisterSceneScreens(UIScreenInstace[] UIScreens);
        public void CleanSceneScreens();
        public void ShowLoadPanel();
        public void HideLoadPanel();
        public void AddContent(Transform panelTr);
    }
}