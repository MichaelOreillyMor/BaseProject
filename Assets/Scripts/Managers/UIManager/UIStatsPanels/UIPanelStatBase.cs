using RPGGame.Units.Stats;

using UnityEngine;
using UnityEngine.UI;

namespace RPGGame.UIsMan.StatsPanels
{ 
    public abstract class UIPanelStatBase : MonoBehaviour
    {
        [SerializeField]
        private Color team1Color;

        [SerializeField]
        private Color team2Color;

        [SerializeField]
        private Image background;

        private IStatState stat;

        public void Setup(IStatState stat, bool isTeam1)
        {
            this.stat = stat;
            stat.AddValueChangeListener(OnValueChange);

            background.color = (isTeam1) ? team1Color : team2Color;
            OnValueChange(stat.Value, stat.InitValue);
        }

        public void Unsetup()
        {
            stat.RemoveValueChangeListener(OnValueChange);
        }

        protected abstract void OnValueChange(int value, int initValue);
    }
}