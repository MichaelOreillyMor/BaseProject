using GFFramework.GameStates.UI;

using GFFramework.UI;
using GFFramework;
using GFFramework.Enums;

using UnityEngine;
using RPGGame.UI.EndGame;

namespace RPGGame.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/GameEndGameState")]
    public class GameEndGameState : BaseUIGameState
    {
        private UIScreenGameEnd endGameScreen;

        protected override void SetUIStateProviders(IGetProvidersRegister reg)
        {

        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is UIScreenGameEnd screen)
            {
                endGameScreen = screen;
                endGameScreen.Setup(OnPlayAgain, LoadNextGameState);
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

        private void OnPlayAgain()
        {
            gameStateProv.LoadGameState(GameStateKey.LoadGameScene);
        }
    }
}