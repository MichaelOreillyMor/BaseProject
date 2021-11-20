using UnityEngine;
using UnityEngine.UI;

namespace RPGGame.UI.HUD
{
    public class UIPanelStatBar : UIPanelStatBase
    {
        [SerializeField]
        Image valueBar;
        protected override void OnValueChange(int value, int initValue)
        {
            valueBar.fillAmount = (float)value / initValue;
        }
    }
}