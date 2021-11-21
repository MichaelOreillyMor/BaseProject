using GFF.PoolsMan.Pools;

using RPGGame.Units.Stats;

using UnityEngine;

namespace RPGGame.UIsMan.StatsPanels
{
    public class UIPanelUnitStats : PoolMember
    {
        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private UIPanelStatBar lifePanel;

        [SerializeField]
        private UIPanelStatText attackPanel;

        [SerializeField]
        private UIPanelStatText actionPointsPanel;

        private Vector3 lastAnchorPosition;
        private Transform anchorPoint;
        private Camera mainCamera;

        private IUnitStatsState unitStats;

        #region Setup methods

        public void Setup(IUnitStatsState unitStats, Transform anchorPoint, bool isTeam1, Camera mainCamera)
        {
            this.anchorPoint = anchorPoint;
            this.mainCamera = mainCamera;
            this.unitStats = unitStats;

            SetupStatsPanels(unitStats, isTeam1);
            unitStats.AddDefeatedListener(Unsetup);

            rectTransform.anchoredPosition = Vector2.zero;
            lastAnchorPosition = Vector3.zero;
        }

        private void SetupStatsPanels(IUnitStatsState unitStats, bool isTeam1)
        {
            lifePanel.Setup(unitStats.GetHealth(), isTeam1);
            attackPanel.Setup(unitStats.GetAttackAction(), isTeam1);
            actionPointsPanel.Setup(unitStats.GetActionPoints(), isTeam1);
        }

        #endregion

        #region Unsetup methods

        private void Unsetup()
        {
            UnsetupStatsPanels();
            unitStats.RemoveDefeatedListener(Unsetup);
            DespawnToPool();
        }

        private void UnsetupStatsPanels()
        {
            lifePanel.Unsetup();
            attackPanel.Unsetup();
            actionPointsPanel.Unsetup();
        }

        #endregion

        #region Position methods

        private void UpdatePosition()
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(anchorPoint.position);
            transform.position = screenPos;
        }

        private void Update()
        {
            if (lastAnchorPosition != anchorPoint.position) 
            {
                UpdatePosition();
            }
        }

        #endregion
    }
}