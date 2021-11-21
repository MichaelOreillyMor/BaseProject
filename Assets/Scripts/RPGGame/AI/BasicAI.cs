using RPGGame.BoardCells;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGGame.SessionsMan.AI
{
    /// <summary>
    /// Very basic AI just to give some feedback to the player
    /// This is a VERY, VERY, VERY bad code, but it works as an example.
    /// </summary>
    public class BasicAI
    {
        private Board board;
        private int numCells;
        private bool isPlayer1;
 
        private List<Cell> cellsAI;
        private List<Cell> cellsEnemy;
        private List<Cell> cellsEmpty;

        private Action<Cell> onSelectCellCallback;

        public BasicAI(Board board, int numAIUnits, int numEnemyUnits, bool isPlayer1, Action<Cell> onSelectCellCallback) 
        {
            this.board = board;
            this.isPlayer1 = isPlayer1;
            this.onSelectCellCallback = onSelectCellCallback;

            numCells = board.GetCellsCount();
            cellsEmpty = new List<Cell>(numCells);
            cellsAI = new List<Cell>(numAIUnits);
            cellsEnemy = new List<Cell>(numEnemyUnits);
        }

        #region AI turn methods

        /// <summary>
        /// This is a very, very, VERY bad code, but it works as an example.
        /// </summary>
        public void Play()
        {
            FillCells();
            PlayAttacks();
            PlayToMoves();
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

        #endregion

        #region move Units methods

        private void PlayToMoves()
        {
            Cell cellEnemy = GetFirstCellEnemy();

            if (cellEnemy != null)
            {
                List<Cell> cellsSorted = cellsEmpty.OrderBy(c => board.GetCellsDistance(cellEnemy, c)).ToList();

                for (int i = 0; i < cellsSorted.Count; i++)
                {
                    Cell cell = cellsSorted[i];
                    TryMoveUnits(cell);
                }
            }
        }

        private void TryMoveUnits(Cell cellEmpty)
        {
            for (int j = 0; j < cellsAI.Count; j++)
            {
                Cell cellAI = cellsAI[j];

                if (cellAI.HasUnit() && !cellEmpty.HasUnit())
                {
                    onSelectCellCallback?.Invoke(cellAI);
                    onSelectCellCallback?.Invoke(cellEmpty);

                    if (cellEmpty.HasUnit())
                    {
                        //The AI was able to move this unit to the cell.
                        //This turn we ended using this Unit.
                        cellsAI.Remove(cellAI);
                        break;
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

        #endregion

        #region attact Units methods

        private void PlayAttacks()
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

        #endregion
    }
}