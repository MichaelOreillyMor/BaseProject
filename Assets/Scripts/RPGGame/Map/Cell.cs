using GFF.InputsMan;
using GFF.PoolsMan.Pools;

using RPGGame.Units;

using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public class Cell : PoolMember, ISelectable
    {
        [SerializeField]
        private Transform anchorPoint;

        private Vector2Int position;
        private UnitState UnitState;

        private Action<Cell> onSelectCell;

        #region Setup methods

        public void Setup(Vector2Int boardPosition)
        {
            position = boardPosition;
        }

        public void SetOnSelectCallback(Action<Cell> callback) 
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

        public bool IsUnitTeam1()
        {
            return (UnitState != null) ? UnitState.IsTeam1 : false;
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

        public Vector2Int GetPosition()
        {
           return position;
        }
    }
}