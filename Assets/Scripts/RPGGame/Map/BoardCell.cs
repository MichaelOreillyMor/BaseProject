using GFFramework.Pools;

using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.Board
{
    public class BoardCell : PoolMember
    {
        public Vector2Int BoardPosition { get; private set; }

        private UnitState unitState;

        private Action<BoardCell> onSelectCell;

        public void Init(Vector2Int boardPosition, Action<BoardCell> onSelectCell)
        {
            BoardPosition = boardPosition;
            this.onSelectCell = onSelectCell;
        }

        public int GetDistance(Vector2Int otherPosition)
        {
            int xDistance = Mathf.Abs(BoardPosition.x - otherPosition.x);
            int yDistance = Mathf.Abs(BoardPosition.y - otherPosition.y);

            return (xDistance > yDistance) ? xDistance : yDistance;
        }

        public bool AddUnit(UnitState unitState)
        {
            if (unitState == null)
            {
                this.unitState = unitState;
                unitState.SetBoardPosition(BoardPosition);
                unitState.transform.position = transform.position;
                return true;
            }

            return false;
        }

        public UnitState RemoveUnit()
        {
            UnitState temp = unitState;
            unitState = null;

            return temp;
        }

        public bool HasUnit()
        {
            return (unitState != null);
        }
    }
}