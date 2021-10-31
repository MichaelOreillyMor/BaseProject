using Game.GameDatas;
using Game.UI;

using GFFramework;
using GFFramework.GameStates;

using UnityEngine;

namespace Game.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/LoadGameState")]
    public class LoadGameState : BaseGameState
    {
        [SerializeField]
        private HUDScreen hudPref;

        [SerializeField]
        private LoadScreen loadScreenPref;
        private LoadScreen loadScreen;

        public override void Setup()
        {
            GameData gameData = dataProv.GetGameData<GameData>();
            loadScreen = uiProv.LoadScreen<LoadScreen>(loadScreenPref);
            loadScreen.Setup();

            playerProv.LoadPlayerController();

            HUDScreen hudScreen = uiProv.LoadHUD<HUDScreen>();
            hudScreen.Setup();

            gameStateProv.LoadGameState(nextGameState);
        }

        public override void Unsetup()
        {
            loadScreen.Unetup();
            uiProv.UnloadScreen();
        }

        public override void Update()
        {
      
        }
    }
}