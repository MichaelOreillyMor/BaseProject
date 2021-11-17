using System.Collections.Generic;
using UnityEngine;

using GFFramework.Pools;

using RPGGame.Board;
using System;

namespace RPGGame.Units
{
    [System.Serializable]
    public class MapBoardFactory : PoolFactory
    {
        [SerializeField]
        private BoardCell cellPref;

        [SerializeField]
        private float cellDistance;

        private Action<BoardCell> onSelectCell;

        public MapBoard CreateMapBoard(Vector2Int mapSize)
        {
            MapBoard mapBoard = new MapBoard();
            onSelectCell = mapBoard.GetOnSelectCellCallback();

            Dictionary<Vector2Int, BoardCell> boardCells = CreateBoardCells(mapSize);

            mapBoard.Init(boardCells);
            return mapBoard;
        }

        private Dictionary<Vector2Int, BoardCell> CreateBoardCells(Vector2Int mapSize)
        {
            int xSize = mapSize.x;
            int ySize = mapSize.y;

            Dictionary<Vector2Int, BoardCell> mapCells = new Dictionary<Vector2Int, BoardCell>(xSize * ySize);
            Vector2Int boardPosition;

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    boardPosition = new Vector2Int(i, j);
                    BoardCell mapCell = CreateCell(boardPosition);
                    mapCells.Add(boardPosition, mapCell);
                }
            }

            return mapCells;
        }

        private BoardCell CreateCell(Vector2Int boardPosition)
        {
            Vector3 worldPosition = new Vector3(boardPosition.x * cellDistance, 0, boardPosition.y * cellDistance);
            BoardCell cell = PoolManager.Spawn(cellPref, worldPosition, Quaternion.identity);

            cell.Init(boardPosition, onSelectCell);
            return cell;
        }
    }
}