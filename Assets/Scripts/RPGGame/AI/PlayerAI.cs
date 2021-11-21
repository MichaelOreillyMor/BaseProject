using RPGGame.BoardCells;

using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGGame.SessionsMan.AI
{
    /// <summary>
    /// Very basic AI just to give some feedback to the player
    /// This is a VERY code, but it works as an example.
    /// </summary>
    public class PlayerAI : IPlayerAI
    {
        private Board board;
        private int numCells;
        private bool isPlayer1;

        private List<ICell> cellsAI;
        private List<ICell> cellsEnemy;
        private List<ICell> cellsEmpty;

        private Action<ICell> onSelectCellCallback;

        #region Setup methods

        public PlayerAI(Board board, int numAIUnits, int numEnemyUnits, bool isPlayer1)
        {
            this.board = board;
            this.isPlayer1 = isPlayer1;

            numCells = board.GetCellsCount();

            cellsEmpty = new List<ICell>(numCells);
            cellsAI = new List<ICell>(numAIUnits);
            cellsEnemy = new List<ICell>(numEnemyUnits);
        }

        public void SetOnSelectCallback(Action<ICell> callback)
        {
            onSelectCellCallback = callback;
        }

        #endregion

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
                ICell cell = board.GetCell(i);

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
            ICell cellEnemy = GetFirstCellEnemy();

            if (cellEnemy != null)
            {
                //This is a VERY code, but it works as an example.
                List<ICell> cellsSorted = cellsEmpty.OrderBy(c => board.GetCellsDistance(cellEnemy, c)).ToList();

                for (int i = 0; i < cellsSorted.Count; i++)
                {
                    ICell cell = cellsSorted[i];
                    TryMoveUnits(cell);
                }
            }
        }

        private void TryMoveUnits(ICell cellEmpty)
        {
            for (int j = 0; j < cellsAI.Count; j++)
            {
                ICell cellAI = cellsAI[j];

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

        private ICell GetFirstCellEnemy()
        {
            for (int i = 0; i < cellsEnemy.Count; i++)
            {
                ICell cellEnemy = cellsEnemy[i];

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
                ICell cellAI = cellsAI[i];

                for (int j = 0; j < cellsEnemy.Count; j++)
                {
                    ICell cellEnemy = cellsEnemy[j];

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