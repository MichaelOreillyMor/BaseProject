using GFF.PoolsMan.Pools;

using RPGGame.Units.Stats;

using UnityEngine;

namespace RPGGame.UIsMan.StatsPanels
{
    /// <summary>
    /// Shows the current StatsState of a UnitState
    /// </summary>
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

        private IReadUnitStatsState unitStats;

        #region Setup methods

        public void Setup(IReadUnitStatsState unitStats, Transform anchorPoint, bool isTeam1, Camera mainCamera)
        {
            this.anchorPoint = anchorPoint;
            this.mainCamera = mainCamera;
            this.unitStats = unitStats;

            SetupStatsPanels(unitStats, isTeam1);
            unitStats.AddDefeatedListener(Unsetup);

            rectTransform.anchoredPosition = Vector2.zero;
            lastAnchorPosition = Vector3.zero;
        }

        private void SetupStatsPanels(IReadUnitStatsState unitStats, bool isTeam1)
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

            anchorPoint = null;
            mainCamera = null;

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