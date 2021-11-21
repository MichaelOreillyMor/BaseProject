using GFF.GameStatesMan.GameStates;
using GFF.RegProviders;
using GFF.UIsMan.UIScreens;
using RPGGame.UIsMan.MainMenu;

using UnityEngine;

namespace RPGGame.GameStatesMan.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/MainMenuGameState")]
    public class MainMenuGameState : BaseUIGameState
    {
        private UIScreenMainMenu mainMenuScreen;

        protected override void SetUIStateProviders(IGetProvidersRegister reg)
        {

        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is UIScreenMainMenu screen)
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