using RPGGame.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGGame.Board
{
    public class MapBoard
    {
        private Dictionary<Vector2Int, BoardCell> cells;

        public void Init(Dictionary<Vector2Int, BoardCell> cells)
        {
            this.cells = cells;
        }

        public Action<BoardCell> GetOnSelectCellCallback() => OnSelectCell;

        public void AddInitUnits(List<UnitState> playerUnits)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                UnitState unitState = playerUnits[i];
                AddUnit(unitState);
            }
        }

        public void AddUnit(UnitState unitState)
        {
            BoardCell boardCell = GetCell(unitState.BoardPosition);

            if (boardCell != null)
            {
                boardCell.AddUnit(unitState);
            }
        }

        public UnitState RemoveUnit(Vector2Int boardPosition)
        {
            BoardCell boardCell = GetCell(boardPosition);

            if (boardCell != null)
            {
                return boardCell.RemoveUnit();
            }

            return null;
        }

        private BoardCell GetCell(Vector2Int boardPosition)
        {
            BoardCell boardCell;
            cells.TryGetValue(boardPosition, out boardCell);
            return boardCell;
        }



        private void OnSelectCell(BoardCell cell)
        {

        }
    }
}