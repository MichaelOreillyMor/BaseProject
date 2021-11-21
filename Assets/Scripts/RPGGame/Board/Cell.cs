using GFF.InputsMan;
using GFF.PoolsMan.Pools;

using RPGGame.Units;

using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public class Cell : PoolMember, ISelectable, ICell
    {
        [SerializeField]
        private Transform anchorPoint;

        private Vector2Int position;
        private IUnitState unitState;

        private Action<ICell> onSelectCell;

        #region Setup methods

        public void Setup(Vector2Int boardPosition)
        {
            position = boardPosition;
        }

        public void SetOnSelectCallback(Action<ICell> callback)
        {
            this.onSelectCell = callback;
        }

        #endregion

        #region Unsetup methods

        public void Unsetup()
        {
            DespawnToPool();
        }

        #endregion

        #region Units methods

        public bool AddUnit(IUnitState unitState)
        {
            if (!HasUnit())
            {
                this.unitState = unitState;
                unitState.SetWorldPosition(anchorPoint.position);
                return true;
            }

            return false;
        }

        public IUnitState RemoveUnit()
        {
            IUnitState temp = unitState;
            unitState = null;

            return temp;
        }

        public bool HasUnit()
        {
            return (unitState != null);
        }

        /// <summary>
        /// If the contentained UnitState belongs to Player 1
        /// </summary>
        public bool IsUnitTeam1()
        {
            return (unitState != null) ? unitState.IsTeam1 : false;
        }

        public IUnitState GetUnit()
        {
            return unitState;
        }

        #endregion

        public void Select()
        {
            onSelectCell?.Invoke(this);
        }

        public Vector2Int GetPosition()
        {
            return position;
        }
    }
}