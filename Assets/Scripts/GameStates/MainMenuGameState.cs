using GFFramework.GameStates.UI;
using Game.UI;

using UnityEngine;
using GFFramework.UI;
using GFFramework;

namespace Game.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/MainMenuGameState")]
    public class MainMenuGameState : BaseUIGameState
    {
        MainMenuScreen mainMenuScreen;

        protected override void SetUIProviders(IGetProvidersRegister reg)
        {

        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is MainMenuScreen screen)
            {
                mainMenuScreen = screen;
                mainMenuScreen.Setup(LoadNextGameState);
            }
            else
            {
                Debug.LogError(name + ": No valid UISreen to load", this);
            }
        }

        protected override void OnPreUIUnsetup()
        {

        }

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            Debug.Log("Quit game");
            Application.Quit();

            return true;
        }
    }
}