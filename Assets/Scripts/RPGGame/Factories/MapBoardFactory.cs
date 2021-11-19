using GFFramework.Pools;

using RPGGame.BoardCells;

using UnityEngine;
using System;

namespace RPGGame.Units
{
    [System.Serializable]
    public class MapBoardFactory : PoolFactory
    {
        [SerializeField]
        private Cell cellPref;

        [SerializeField]
        private float cellDistance;

        public Board CreateBoard(Vector2Int boardSize)
        {
            Board mapBoard = new Board();
            Cell[] boardCells = CreateBoardCells(boardSize);

            mapBoard.Init(boardCells, boardSize);
            return mapBoard;
        }

        private Cell[] CreateBoardCells(Vector2Int mapSize)
        {
            int xSize = mapSize.x;
            int ySize = mapSize.y;

            Cell[] mapCells = new Cell[xSize * ySize];
            Vector2Int boardPosition;

            for (int yPos = 0; yPos < xSize; yPos++)
            {
                for (int xPos = 0; xPos < ySize; xPos++)
                {
                    boardPosition = new Vector2Int(yPos, xPos);
                    Cell mapCell = CreateCell(boardPosition);
                    mapCells[(yPos * xSize) + xPos] = mapCell;
                }
            }

            return mapCells;
        }

        private Cell CreateCell(Vector2Int boardPosition)
        {
            Vector3 worldPosition = new Vector3(boardPosition.x * cellDistance, 0, boardPosition.y * cellDistance);
            Cell cell = PoolManager.Spawn(cellPref, worldPosition, Quaternion.identity);

            cell.Init(boardPosition);
            return cell;
        }
    }
}