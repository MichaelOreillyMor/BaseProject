using GFF.UIsMan.UIScreens;

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPGGame.UIsMan.HUD
{
    public class UIScreenHUD : BaseUIScreen
    {
        [SerializeField]
        private Button onEndTurnBttn;

        [SerializeField]
        private Button onSurrenderBttn;

        private Action onEndTurnCallback;
        private Action onSurrenderCallback;

        public void Setup(Action onEndTurnCallback, Action onSurrenderCallback)
        {
            this.onEndTurnCallback = onEndTurnCallback;
            this.onSurrenderCallback = onSurrenderCallback;

            onEndTurnBttn.onClick.RemoveAllListeners();
            onEndTurnBttn.onClick.AddListener(OnEndTurnClick);

            onSurrenderBttn.onClick.RemoveAllListeners();
            onSurrenderBttn.onClick.AddListener(OnSurrenderClick);
        }

        public override void Unsetup()
        {
            onEndTurnBttn.onClick.RemoveAllListeners();
            onSurrenderBttn.onClick.RemoveAllListeners();
        }

        private void OnEndTurnClick() 
        {
            onEndTurnCallback?.Invoke();
        }

        private void OnSurrenderClick()
        {
            onSurrenderCallback?.Invoke();
        }
    }
}