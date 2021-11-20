using UnityEngine;
using TMPro;
namespace RPGGame.UI.HUD
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