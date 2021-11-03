using Game.UI;
using GFFramework.GameStates.UI;
using GFFramework.UI;

using UnityEngine;

namespace Game.GameStates
{
    /// <summary>
    /// This state handles the load of the persistent resources just needed for playing the game (all states different from UIMainMenu)
    /// An oposite state to unload this resources would be needed in a real game.
    /// </summary>
    [CreateAssetMenu(menuName = "GameStates/LoadGameState")]
    public class LoadGameState : BaseUIGameState
    {
        private LoadScreen loadScreen;

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is LoadScreen screen)
            {
                loadScreen = screen;
                loadScreen.Setup();

                HUDScreen hudScreen = uiProv.LoadHUD<HUDScreen>();
                hudScreen.Setup();

                reg.PlayerProv.LoadPlayerController();
                LoadNextGameState();
            }
        }

        protected override void OnPreUIUnsetup()
        {

        }


        protected override void OnUpdate()
        {
      
        }
    }
}