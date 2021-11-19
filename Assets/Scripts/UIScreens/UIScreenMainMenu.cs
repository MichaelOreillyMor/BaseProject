using GFFramework.UI;

using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPGGame.UI.MainMenu
{
    public class UIScreenMainMenu : BaseUIScreen
    {
        [SerializeField]
        private Button startGameButton;

        private Action onStartCallback;

        public void Setup(Action onStartCallback)
        {
            this.onStartCallback = onStartCallback;

            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(OnClickStart);
        }

        public override void Unsetup()
        {
            //TO_DO
        }

        private void OnClickStart()
        {
            onStartCallback?.Invoke();
        }
    }
}
