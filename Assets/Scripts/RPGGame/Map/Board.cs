using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public class Board
    {
        private Cell[] cells;

        private int cellsCount;
        private Vector2Int size;

        public void Init(Cell[] cells, Vector2Int size)
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

        public void AddUnit(UnitState unitState, Vector2Int boardPosition)
        {
            Cell boardCell = GetCell(boardPosition);

            if (boardCell != null)
            {
                boardCell.AddUnit(unitState);
            }
        }

        public UnitState RemoveUnit(Vector2Int boardPosition)
        {
            Cell boardCell = GetCell(boardPosition);

            if (boardCell != null)
            {
                return boardCell.RemoveUnit();
            }

            return null;
        }

        private Cell GetCell(Vector2Int boardPosition)
        {
            int index = (boardPosition.y * size.x) + boardPosition.x;

            if (index < cellsCount)
                return cells[index];

            return null;
        }

        public int GetCellsDistance(Cell cellA, Cell cellB)
        {
            Vector2Int posA = cellA.position;
            Vector2Int posB = cellB.position;

            int xDistance = Mathf.Abs(posA.x - posB.x);
            int yDistance = Mathf.Abs(posA.y - posB.y);

            return (xDistance > yDistance) ? xDistance : yDistance;
        }
    }
}