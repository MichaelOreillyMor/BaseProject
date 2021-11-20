using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public class Board
    {
        private Cell[] cells;

        private Vector2Int size;
        private int cellsCount;

        #region Setup methods

        public Board(Cell[] cells, Vector2Int size)
        {
            this.cells = cells;
            this.size = size;

            cellsCount = cells.Length;
        }

        public void SetOnSelectCallback(Action<Cell> callback)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                Cell cell = cells[i];
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

        public void AddUnit(UnitState unitState, Vector2Int boardPosition)
        {
            Cell boardCell = GetCell(boardPosition);

            if (boardCell != null)
            {
                boardCell.AddUnit(unitState);
            }
        }

        public void RemoveUnit(Cell cell)
        {
            if (cell != null)
            {
                cell.RemoveUnit();
            }
        }

        public void MoveUnit(Cell cellA, Cell cellB)
        {
            if (cellA && cellB && !cellB.HasUnit())
            {
                UnitState unit = cellA.RemoveUnit();

                if (unit)
                {
                    cellB.AddUnit(unit);
                }  
            }
        }

        #endregion

        #region Cells methods

        public Cell GetCell(Vector2Int boardPosition)
        {
            int index = (boardPosition.y * size.x) + boardPosition.x;

            if (index < cellsCount)
                return cells[index];

            return null;
        }

        public Cell GetCell(int index)
        {
            if (index < cellsCount)
                return cells[index];

            return null;
        }

        public int GetCellsCount() 
        {
            return cellsCount;
        }

        public int GetCellsDistance(Cell cellA, Cell cellB)
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