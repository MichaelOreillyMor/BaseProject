using GFFramework.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenuScreen : BaseUIScreen
    {
        [SerializeField]
        Button startGameButton;

        public void Setup(Action onClickStart)
        {
            if (!IsInit)
            {
                startGameButton.onClick.RemoveAllListeners();
                startGameButton.onClick.AddListener(() => onClickStart());
                OnSetup();
            }
        }

        protected override void OnUnsetup()
        {
            //TO_DO
        }
    }
}
