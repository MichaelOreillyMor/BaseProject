using GFFramework.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPGGame.UI.EndGame
{
    public class UIScreenGameEnd : BaseUIScreen
    {
        [SerializeField]
        private Button onPlayAgainBttn;

        [SerializeField]
        private Button onReturnMainMenuBttn;

        private Action onPlayAgainCallback;
        private Action onReturnMainMenuCallback;

        public void Setup(Action onPlayAgainCallback, Action onReturnMainMenuCallback)
        {
            this.onPlayAgainCallback = onPlayAgainCallback;
            this.onReturnMainMenuCallback = onReturnMainMenuCallback;

            onPlayAgainBttn.onClick.RemoveAllListeners();
            onPlayAgainBttn.onClick.AddListener(OnPlayAgainClick);

            onReturnMainMenuBttn.onClick.RemoveAllListeners();
            onReturnMainMenuBttn.onClick.AddListener(OnReturnMainMenuClick);
        }

        public override void Unsetup()
        {
            onPlayAgainBttn.onClick.RemoveAllListeners();
            onReturnMainMenuBttn.onClick.RemoveAllListeners();
        }

        private void OnPlayAgainClick() 
        {
            onPlayAgainCallback?.Invoke();
        }

        private void OnReturnMainMenuClick()
        {
            onReturnMainMenuCallback?.Invoke();
        }
    }
}