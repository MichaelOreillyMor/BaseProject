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
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(() => onClickStart());
        }

        public override void Unsetup()
        {
            //TO_DO
        }
    }
}
