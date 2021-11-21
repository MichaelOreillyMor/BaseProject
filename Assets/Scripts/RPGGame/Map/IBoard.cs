using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public interface IBoard
    {
        bool AddUnit(IUnitState unitState, Vector2Int boardPosition);
        ICell GetCell(int index);
        ICell GetCell(Vector2Int boardPosition);
        int GetCellsCount();
        int GetCellsDistance(ICell cellA, ICell cellB);
        bool MoveUnit(ICell cellA, ICell cellB);
        void RemoveUnit(ICell cell);
        void SetOnSelectCallback(Action<ICell> callback);
        void Unsetup();
    }
}