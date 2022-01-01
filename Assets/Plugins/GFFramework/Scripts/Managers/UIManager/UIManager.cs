using GFF.GameStatesMan.Keys;
using GFF.ScenesMan.Utils;
using GFF.ServiceLocators;
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
    public class UIManager : BaseGameManager, IUIManager
    {
        [SerializeField]
        private UIMainPanel mainPanel;

        private Dictionary<int, BaseUIScreen> sceneScreens;
        private BaseUIScreen currentScreen;

        #region Setup/Unsetup methods

        public override void Setup(ISetService serviceLocator, Action onNextSetupCallback)
        {
            SetService(serviceLocator);

            sceneScreens = new Dictionary<int, BaseUIScreen>();
            ShowLoadPanel();

            onNextSetupCallback?.Invoke();
        }

        protected override void SetService(ISetService serviceLocator)
        {
            serviceLocator.SetService<IUIManager>(this);
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
                BaseUIScreen screenInstance = GetScreenInstance(screenPref);

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
            Debug.LogError("You are creating a UIScreen:  " + screenPref.name + " instance on run-time, consider the creation of one instance inside the Ref SceneScreens");
            BaseUIScreen screenInstance = Instantiate(screenPref);
            return screenInstance;
        }

        public BaseUIScreen GetScreenInstance(BaseUIScreen prefUIScreen)
        {
            BaseUIScreen screenInstance;
            sceneScreens.TryGetValue(prefUIScreen.GetInstanceID(), out screenInstance);

            return screenInstance;
        }

        public void UnloadUIScreen()
        {
            if (currentScreen != null)
            {
                currentScreen.Unsetup();

                if (HasScreenInstance(currentScreen))
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

        private bool HasScreenInstance(BaseUIScreen UIScreen)
        {
            return sceneScreens.ContainsValue(UIScreen);
        }

        public void RegisterSceneScreens(UIScreenInstace[] UIScreens)
        {
            CleanSceneScreens();

            if (UIScreens != null)
            {
                for (int i = 0; i < UIScreens.Length; i++)
                {
                    UIScreenInstace instance = UIScreens[i];

                    if (instance.UIScreen != null)
                    {
                        sceneScreens.Add(instance.PrefID, instance.UIScreen);
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
