using GFFramework.Pools;

using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public class Cell : PoolMember
    {
        [SerializeField]
        private Transform anchorPoint;

        public Vector2Int position;
        private UnitState UnitState;

        private Action<Cell> onSelectCell;

        #region Init methods

        public void Init(Vector2Int boardPosition)
        {
            position = boardPosition;
        }

        public void SetOnSelectCallback(Action<Cell> callback) 
        {
            this.onSelectCell = callback;
        }

        #endregion

        #region Units methods

        public bool AddUnit(UnitState unitState)
        {
            if (unitState != null)
            {
                this.UnitState = unitState;
                unitState.transform.position =  anchorPoint.position;
                return true;
            }

            return false;
        }

        public UnitState RemoveUnit()
        {
            UnitState temp = UnitState;
            UnitState = null;

            return temp;
        }

        public bool HasUnit()
        {
            return (UnitState != null);
        }

        public UnitState GetUnit()
        {
            return UnitState;
        }

        #endregion

        public void Select()
        {
            onSelectCell?.Invoke(this);
        }

        public Vector2Int GetBoardPosition()
        {
           return position;
        }
    }
}