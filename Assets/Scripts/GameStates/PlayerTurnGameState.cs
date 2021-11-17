using RPGGame.UI;

using GFFramework;
using GFFramework.Enums;
using GFFramework.GameStates.UI;
using GFFramework.UI;

using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace RPGGame.GameStates
{
    [CreateAssetMenu(menuName = "GameStates/PlayerTurnGameState")]
    public class PlayerTurnGameState : BaseUIGameState
    {
        private HUDScreen HUDScreen;

        private IInputProvider inputProv;
        private  IRPGGameSessionProvider sessionProv;

        #region Setup/Unsetup methods

        protected override void SetUIStateProviders(IGetProvidersRegister reg)
        {
            uiProv = reg.UIProv;
            inputProv = reg.InputProv;
            sessionProv = (IRPGGameSessionProvider)reg.GameSessionProv;
        }

        protected override void OnPostUILoaded(BaseUIScreen uiScreen)
        {
            if (uiScreen is HUDScreen screen)
            {
                HUDScreen = screen;
                HUDScreen.Setup();
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

        private void OnPointSelect(Vector3 worldPoint) 
        {
            Debug.Log(worldPoint);
        }

        public override void Update()
        {

        }

        public override bool OnBack()
        {
            CloseSession();
            return true;
        }

        private void CloseSession()
        {
            sessionProv.EndSession();
            gameStateProv.LoadGameState(GameStateKey.LoadMainMenuScene);
        }
    }
}