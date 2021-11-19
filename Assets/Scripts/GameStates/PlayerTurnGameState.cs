using RPGGame.UI.HUD;

using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates.UI;
using GFFramework.UI;

using UnityEngine;

namespace RPGGame.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/PlayerTurnGameState")]
    public class PlayerTurnGameState : BaseUIGameState
    {
        private UIScreenHUD HUDScreen;

        private IInputProvider inputProv;
        private IRPGGameSessionProvider sessionProv;

        #region Setup/Unsetup methods

        protected override void SetUIStateProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is UIScreenHUD screen)
            {
                HUDScreen = screen;
                HUDScreen.Setup(OnEndTurn, OnSurrender);
                inputProv.SetSelectWorldPointCalback(OnPointSelect);
            }
            else
            {
                Debug.LogError(name + ": No valid UISreen to load", this);
            }
        }

        protected override void OnPreUIUnsetup()
        {
            inputProv.RemovetWorldPointCalback();
        }

        #endregion

        private void OnEndTurn()
        {
            LoadNextGameState();
        }

        private void OnSurrender()
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.LoseMenu);
        }

        private void OnPointSelect(Vector3 worldPoint) 
        {
            Debug.Log(worldPoint);
        }

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            OnCloseSession();
            return true;
        }

        private void OnCloseSession()
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.LoadMainMenuScene);
        }
    }
}