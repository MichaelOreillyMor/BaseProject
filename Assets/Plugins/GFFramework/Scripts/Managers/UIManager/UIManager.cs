using GFF.Enums;
using GFF.RegProviders;
using GFF.UIsMan.Panels;
using GFF.UIsMan.UIScreens;

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GFF.UIsMan
{
    /// <summary>
    /// Handles the UIScreens instantiation . It contains a dictionary of the 
    /// UIScreens that are part of the scene and don´t need to be instantiated on run-time
    /// If you want a persistent UIScreen: add it to this prefab (right now is just LoadScreen WIP)
    /// If you want a persistent UIScreen during the life time of a scene: add it to SceneInfo prefab
    /// If you want a UIScreen loaded in run-time that will be destroyed: just add the reference to the GameState
    /// </summary>
    public class UIManager : BaseGameManager, IUIProvider
    {
        [SerializeField]
        private UIMainPanel mainPanel;

        private Dictionary<GameStateKey, BaseUIScreen> sceneScreens;
        private BaseUIScreen currentScreen;

        #region Setup/Unsetup methods

        public override void Setup(ISetProvidersRegister reg, Action onNextSetupCallback)
        {
            reg.UIProv = this;

            sceneScreens = new Dictionary<GameStateKey, BaseUIScreen>();
            ShowLoadPanel();

            Debug.Log("Setup UIManager");
            onNextSetupCallback?.Invoke();
        }

        public override void Unsetup()
        {
            UnloadUIScreen();

            if (sceneScreens != null)
            {
                sceneScreens.Clear();
            }

            Debug.Log("Unsetup UIManager");
        }

        #endregion

        #region UIScreens control methods

        public BaseUIScreen LoadUIScreen(BaseUIScreen screenPref)
        {
            UnloadUIScreen();

            if (screenPref)
            {
                BaseUIScreen screenInstance = GetScreenInstance(screenPref.Owner);

                if (screenInstance != null)
                {
                    screenInstance.Show(true);
                }
                else 
                {
                    screenInstance = CreateScreenInstance(screenPref);
                }

                currentScreen = screenInstance;
                return currentScreen;
            }

            return null;
        }

        private BaseUIScreen CreateScreenInstance(BaseUIScreen screenPref)
        {
            Debug.LogError("You are creating a UIScreen (" + screenPref.Owner + ") instance on run-time, consider the creation of one instance inside the Ref SceneScreens");
            BaseUIScreen screenInstance = Instantiate(screenPref);
            return screenInstance;
        }

        public BaseUIScreen GetScreenInstance(GameStateKey owner)
        {
            BaseUIScreen screenInstance;
            sceneScreens.TryGetValue(owner, out screenInstance);

            return screenInstance;
        }

        public void UnloadUIScreen()
        {
            if (currentScreen != null)
            {
                currentScreen.Unsetup();

                if (HasScreenInstance(currentScreen.Owner))
                {
                    currentScreen.Show(false);
                }
                else
                {
                    Destroy(currentScreen.gameObject);
                }

                currentScreen = null;
            }
        }

        #endregion

        #region MainPanel methods

        public void ShowLoadPanel()
        {
            mainPanel.FadeIn();
        }

        public void HideLoadPanel()
        {
            mainPanel.FadeOut();
        }

        public void AddContent(Transform panelTr)
        {
            mainPanel.AddContent(panelTr);
        }

        #endregion

        public bool HasScreenInstance(GameStateKey owner)
        {
            return sceneScreens.ContainsKey(owner);
        }

        public void RegisterSceneScreens(BaseUIScreen[] UIScreens)
        {
            CleanSceneScreens();

            if (UIScreens != null)
            {
                for (int i = 0; i < UIScreens.Length; i++)
                {
                    BaseUIScreen UIScreen = UIScreens[i];
                    GameStateKey owner;

                    if (UIScreen != null)
                    {
                        owner = UIScreen.Owner;

                        if (owner != GameStateKey.None)
                        {
                            sceneScreens.Add(owner, UIScreen);
                        }
                        else
                        {
                            Debug.LogError(UIScreen.name + " doesn't have a Gamestate owner", UIScreen);
                        }
                    }
                }
            }
        }

        public void CleanSceneScreens() 
        {
            sceneScreens.Clear();
        }
    }
}
