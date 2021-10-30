using GFFramework;
using GFFramework.UI;
using Game.UI;

using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "GameStates/MainMenuGameState")]
    public class MainMenuGameState : BaseGameState
    {
        [SerializeField]
        MainMenuScreen mainMenuScreenPref;
        MainMenuScreen mainMenuScreen;

        public override void Setup()
        {
            mainMenuScreen = uiProv.LoadScreen<MainMenuScreen>(mainMenuScreenPref);
            mainMenuScreen.Setup(LoadNextState);
            inputProv.SetUIbacks(mainMenuScreen);
        }

        public override void Unsetup()
        {
            inputProv.RemoveUIbacks();
            mainMenuScreen.Unetup();
            uiProv.UnloadScreen();
        }

        public override void Update()
        {

        }
    }
}