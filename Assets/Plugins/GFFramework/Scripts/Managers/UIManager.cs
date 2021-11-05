using System;
using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// Handles the UIScreens instantiation, and keeps a reference to the HUD, the only UIScreen that is persistent for now
    /// An improvement is going be to avoid the constant destruction rest of screens that are not the HUD.
    /// 
    /// TO-DO:
    /// Each scene will have a script SceneController : Mono
    /// The sceneInfo will have a ScreensRefs.cs (serialize class)
    /// When the scene is loaded callback to the LoadGameState
    /// In the State uiMan.Set(sceneController.ScreenProv)
    /// 
    /// Each UIScreen will have the same Enum that its state
    /// UIManager will load a dictionary of Enums + Scenes using ScreensRefs
    /// Hud refs here will be removed
    /// 
    /// When a new scene is loaded
    /// we have to clean the GameStateMan, prevState null
    /// we have to clean the UIMan, currentScreen null
    /// IdleState will be a UIState, HUD will be loaded in the Setup
    /// sceneController has to be a prefab viarian in each scene
    /// </summary>
    public class UIManager : BaseGameManager, IUIProvider
    {
        [SerializeField]
        private BaseUIScreen hudScreenPref;
        private BaseUIScreen hudScreen;

        private BaseUIScreen currentScreen;

        #region IGameManager

        public override void Setup(ISetProvidersRegister reg, Action onNextSetup)
        {
            reg.UIProv = this;

            Debug.Log("Setup UIManager");
            onNextSetup?.Invoke();
        }

        public override void Unsetup()
        {
            UnloadScreen();
            UnloadHUD();
            Debug.Log("Unsetup UIManager");
        }

        #endregion

        public BaseUIScreen LoadScreen(BaseUIScreen screenPref)
        {
            UnloadScreen();

            if (screenPref)
            {
                currentScreen = Instantiate(screenPref, transform);
                currentScreen.transform.SetAsLastSibling();
                return currentScreen;
            }

            return null;
        }

        public void UnloadScreen()
        {
            if (currentScreen != null)
            {
                currentScreen.Unsetup();
                Destroy(currentScreen.gameObject);//Destroy is bad is a WIP
                currentScreen = null;
            }
        }

        public T LoadHUD<T>() where T : BaseUIScreen
        {
            if (hudScreenPref)
            { 
                if (hudScreen != null)
                {
                    hudScreen.Unsetup();
                    UnloadHUD();
                }

                hudScreen = Instantiate(hudScreenPref, transform);//Instantiate one by one is bad is a WIP
                hudScreen.transform.SetAsFirstSibling();
            }

            return (T)hudScreen;
        }

        public void UnloadHUD()
        {
            if (hudScreen != null)
            {
                hudScreen.Unsetup();
                Destroy(hudScreen);
            }
        }

        public BaseUIScreen ShowHUD(bool active)
        {
            if (hudScreen != null)
            {
                hudScreen.gameObject.SetActive(active);
            }

            return hudScreen;
        }
    }
}
