using GFFramework.Pools;

using RPGGame.Units.Stats;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPGGame.UI.HUD
{
    public class UIPanelUnitStats : PoolMember
    {
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private Image background;

        [SerializeField]
        private Color team1Color;

        [SerializeField]
        private Color team2Color;

        private IUnitStatsState unitStats;
        private Transform anchorPoint;

        public void Init(IUnitStatsState unitStats, Transform anchorPoint, bool isTeam1)
        {
            this.unitStats = unitStats;
            this.anchorPoint = anchorPoint;

            background.color = (isTeam1) ? team1Color : team2Color;
            rectTransform.anchoredPosition = Vector2.zero;

            unitStats.AddDefeatedListener(Despawn);
        }

        private void RemoveAllListeners()
        {
            unitStats.RemoveDefeatedListener(Despawn);
        }

        public void Despawn()
        {
            RemoveAllListeners();
            DespawnToPool();
        }
    }
}