using GFF.GameStatesMan.GameStates;
using GFF.ServiceLocators;
using GFF.UIsMan.UIScreens;
using GFF.Enums;

using RPGGame.UIsMan.GameEnds;

using UnityEngine;


namespace RPGGame.GameStatesMan.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/GameEndGameState")]
    public class GameEndGameState : BaseUIGameState
    {
        private UIScreenGameEnd endGameScreen;

        protected override void SetUIStateServices(IGetService serviceLocator)
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
            gameStateMan.LoadGameState(GameStateKey.LoadGameScene);
        }
    }
}