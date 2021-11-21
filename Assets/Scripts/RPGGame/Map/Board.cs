using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public class Board : IBoard
    {
        private ICell[] cells;

        private Vector2Int size;
        private int cellsCount;

        #region Setup methods

        public Board(ICell[] cells, Vector2Int size)
        {
            this.cells = cells;
            this.size = size;

            cellsCount = cells.Length;
        }

        /// <summary>
        /// Set in the Cells the callback that they should call when they are selected
        /// </summary>
        public void SetOnSelectCallback(Action<ICell> callback)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                ICell cell = cells[i];
                cell.SetOnSelectCallback(callback);
            }
        }

        #endregion

        #region Unsetup methods

        public void Unsetup()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Unsetup();
            }
        }

        #endregion

        #region Units methods

        /// <summary>
        /// Adds a UnitState to a Board position if it´s empty
        /// </summary>
        public bool AddUnit(IUnitState unitState, Vector2Int boardPosition)
        {
            ICell cell = GetCell(boardPosition);

            if (cell != null)
            {
                return cell.AddUnit(unitState);
            }

            return false;
        }

        /// <summary>
        /// Removes a Unit from a Cell
        /// </summary>
        public void RemoveUnit(ICell cell)
        {
            if (cell != null)
            {
                cell.RemoveUnit();
            }
        }

        /// <summary>
        /// Moves a UnitState from one cell to the other one if it´s possible
        /// </summary>
        public bool MoveUnit(ICell cellA, ICell cellB)
        {
            if (cellA != null && cellB != null && !cellB.HasUnit())
            {
                IUnitState unit = cellA.RemoveUnit();

                if (unit != null)
                {
                    return cellB.AddUnit(unit);
                }
            }

            return false;
        }

        #endregion

        #region Cells methods

        public ICell GetCell(Vector2Int boardPosition)
        {
            int index = (boardPosition.y * size.x) + boardPosition.x;

            if (index < cellsCount)
                return cells[index];

            return null;
        }

        public ICell GetCell(int index)
        {
            if (index < cellsCount)
                return cells[index];

            return null;
        }

        public int GetCellsCount()
        {
            return cellsCount;
        }

        public int GetCellsDistance(ICell cellA, ICell cellB)
        {
            Vector2Int posA = cellA.GetPosition();
            Vector2Int posB = cellB.GetPosition();

            int xDistance = Mathf.Abs(posA.x - posB.x);
            int yDistance = Mathf.Abs(posA.y - posB.y);

            return (xDistance > yDistance) ? xDistance : yDistance;
        }

        #endregion
    }
}