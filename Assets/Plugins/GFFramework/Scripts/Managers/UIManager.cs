using System;
using UnityEngine;

namespace GFFramework.UI
{
    /// <summary>
    /// Handles the UIScreens instantiation, and keeps a reference to the HUD the only UIScreens that is persistent for now
    /// An improvement is going be to avoid the constant destruction rest of screens that are not the HUD.
    /// Some are going to be persistent and others temporary, depending on a flag in the BaseScreen.
    /// Some of the persistent screens would keep their state during the whole game.
    /// The direct reference to the HUD would be removed.
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
            reg.SetUI(this);
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

        public T LoadScreen<T>(BaseUIScreen screenPref) where T : BaseUIScreen
        {
            UnloadScreen();

            if (screenPref)
            {
                currentScreen = Instantiate(screenPref, transform);
                currentScreen.transform.SetAsLastSibling();
                return (T)currentScreen;
            }

            return null;
        }

        public void UnloadScreen()
        {
            if (currentScreen != null)
            {
                currentScreen.Unetup();
                Destroy(currentScreen.gameObject);
                currentScreen = null;
            }
        }

        public T LoadHUD<T>() where T : BaseUIScreen
        {
            if (hudScreenPref)
            { 
                if (hudScreen != null)
                {
                    hudScreen.Unetup();
                    UnloadHUD();
                }

                hudScreen = Instantiate(hudScreenPref, transform);
                hudScreen.transform.SetAsFirstSibling();
            }

            return (T)hudScreen;
        }

        public void UnloadHUD()
        {
            if (hudScreen != null)
            {
                hudScreen.Unetup();
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
