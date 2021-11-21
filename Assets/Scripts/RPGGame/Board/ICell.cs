using RPGGame.Units;
using System;
using UnityEngine;

namespace RPGGame.BoardCells
{
    public interface ICell
    {
        bool AddUnit(IUnitState unitState);
        Vector2Int GetPosition();
        IUnitState GetUnit();
        bool HasUnit();
        bool IsUnitTeam1();
        IUnitState RemoveUnit();
        void SetOnSelectCallback(Action<ICell> callback);
        void Setup(Vector2Int boardPosition);
        void Unsetup();
    }
}