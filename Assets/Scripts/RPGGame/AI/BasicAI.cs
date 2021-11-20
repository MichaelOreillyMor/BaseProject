using RPGGame.BoardCells;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGGame.GameSession.AI
{
    /// <summary>
    /// Very basic AI just to give some feedback to the player
    /// This is a very, very, VERY bad code, but it works as an example.
    /// Please don´t read it
    /// </summary>
    public class BasicAI
    {
        private Board board;
        private int numCells;
        private bool isPlayer1;
 
        private List<Cell> cellsAI;
        private List<Cell> cellsEnemy;
        private List<Cell> cellsEmpty;
        private List<Cell> sortedEmptyCells;

        private Action<Cell> onSelectCellCallback;

        public BasicAI(Board board, int numUnits, bool isPlayer1, Action<Cell> onSelectCellCallback) 
        {
            this.board = board;
            this.isPlayer1 = isPlayer1;
            this.onSelectCellCallback = onSelectCellCallback;

            numCells = board.GetCellsCount();
            cellsEnemy = new List<Cell>(numUnits);
            cellsAI = new List<Cell>(numUnits);
            cellsEmpty = new List<Cell>(numCells);
        }

        public void Play()
        {
            FillCells();
            TryToAttack();
            TryToMove();
        }

        /// <summary>
        /// This is a very, very, VERY bad code, but it works as an example.
        /// Please don´t read it
        /// </summary>
        private void TryToMove()
        {
            Cell cellEnemy = GetFirstCellEnemy();

            if (cellEnemy != null)
            {
                sortedEmptyCells = cellsEmpty.OrderBy(c => board.GetCellsDistance(cellEnemy, c)).ToList();

                for (int i = 0; i < sortedEmptyCells.Count; i++)
                {
                    Cell cellEmpty = sortedEmptyCells[i];

                    for (int j = 0; j < cellsAI.Count; j++)
                    {
                        Cell cellAI = cellsAI[j];

                        if (cellAI.HasUnit())
                        {
                            onSelectCellCallback?.Invoke(cellAI);
                            onSelectCellCallback?.Invoke(cellEmpty);
                        }
                        else 
                        {
                            cellsAI.Remove(cellAI);
                            break;
                        }
                    }
                }
            }
        }

        private Cell GetFirstCellEnemy()
        {
            for (int i = 0; i < cellsEnemy.Count; i++)
            {
                Cell cellEnemy = cellsEnemy[i];

                if (cellEnemy.HasUnit() && isPlayer1 != cellEnemy.IsUnitTeam1())
                {
                    return cellEnemy;
                }
            }

            return null;
        }

        private void TryToAttack()
        {
            for (int i = 0; i < cellsAI.Count; i++)
            {
                Cell cellAI = cellsAI[i];

                for (int j = 0; j < cellsEnemy.Count; j++)
                {
                    Cell cellEnemy = cellsEnemy[j];

                    if (cellEnemy.HasUnit())
                    {
                        onSelectCellCallback?.Invoke(cellAI);
                        onSelectCellCallback?.Invoke(cellEnemy);
                    }

                }
            }
        }

        private void FillCells()
        {
            cellsAI.Clear();
            cellsEnemy.Clear();
            cellsEmpty.Clear();

            for (int i = 0; i < numCells; i++)
            {
                Cell cell = board.GetCell(i);

                if (cell.HasUnit())
                {
                    if (isPlayer1 == cell.IsUnitTeam1())
                    {
                        cellsAI.Add(cell);
                    }
                    else
                    {
                        cellsEnemy.Add(cell);
                    }
                }
                else 
                {
                    cellsEmpty.Add(cell);
                }
            }
        }
    }
}