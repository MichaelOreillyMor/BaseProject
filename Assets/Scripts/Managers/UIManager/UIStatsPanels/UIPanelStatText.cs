using UnityEngine;
using TMPro;

namespace RPGGame.UIsMan.StatsPanels
{
    public class UIPanelStatText : UIPanelStatBase
    {
        [SerializeField]
        TMP_Text valueTxt;

        protected override void OnValueChange(int value, int initValue)
        {
            valueTxt.text = value.ToString();
        }
    }
}