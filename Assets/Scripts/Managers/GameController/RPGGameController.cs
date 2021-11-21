using GFF.SessionsMan.TurnBasedSessions;

using RPGGame.BoardCells;
using RPGGame.SessionsMan.AI;
using RPGGame.Units;

using System;
using System.Collections.Generic;

namespace RPGGame.SessionsMan
{
    public class RPGGameController : ITurnBasedGameController
    {
        protected bool GameStarted { get; private set; }

        private Board board;
        private BasicAI basicAI;

        private List<UnitState> player1Units;
        private List<UnitState> player2Units;

        private UnitState unitSelect;
        private Cell cellUnitSelect;
        private Cell cellAttacked;

        private bool isPlayer1Turn;

        private Action onWinGameCallback;

        #region Setup methods

        public RPGGameController(Board board, List<UnitState> player1Units, List<UnitState> player2Units)
        {
            this.board = board;
            this.player1Units = player1Units;
            this.player2Units = player2Units;

            board.SetOnSelectCallback(OnSelectCell);
            basicAI = new BasicAI(board, player2Units.Count, player1Units.Count, false, OnSelectCell);
        }

        public void StartGame()
        {
            if (!GameStarted)
            {
                isPlayer1Turn = true;
                GameStarted = true;
            }
        }

        #endregion

        #region Unsetup methods

        public void EndGame()
        {
            if (GameStarted)
            {
                board.Unsetup();
                UnsetupUnits(player1Units);
                UnsetupUnits(player2Units);
                GameStarted = false;
            }
        }

        private void UnsetupUnits(List<UnitState> playerUnits)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                playerUnits[i].Unsetup();
            }
        }

        #endregion

        #region Select cells methods

        private void OnSelectCell(Cell cell)
        {
            if (cell.HasUnit())
            {
                bool isPlayerUnit = (cell.IsUnitTeam1() == isPlayer1Turn);

                if (isPlayerUnit)
                {
                    SelectUnitCell(cell);
                }
                else
                {
                    TryAttackUnit(cell);
                }
            }
            else
            {
                TryMoveUnit(cell);
            }
        }

        private void SelectUnitCell(Cell cell)
        {
            cellUnitSelect = cell;
            unitSelect = (cellUnitSelect) ? cellUnitSelect.GetUnit() : null;
        }

        #endregion

        #region Player actions methods

        private void TryMoveUnit(Cell cell)
        {
            if (cellUnitSelect && cell && !cell.HasUnit())
            {
                int distance = GetCellUnitDistance(cell);
                if (unitSelect.TryMovePosition(distance))
                {
                    board.MoveUnit(cellUnitSelect, cell);
                    SelectUnitCell(null);
                }
            }
        }

        private void TryAttackUnit(Cell cell)
        {
            if (cellUnitSelect && cell && cell.HasUnit())
            {
                UnitState enemyUnit = cell.GetUnit();
                int distance = GetCellUnitDistance(cell);
                cellAttacked = cell;

                if (unitSelect.TryAttackUnit(enemyUnit, distance, OnDeadUnit))
                {
                    SelectUnitCell(null);
                }
            }
        }

        #endregion

        #region Dead unit methods

        private void OnDeadUnit(UnitState enemyUnit)
        {
            List<UnitState> enemyUnits = GetEnemyUnits();
            if (enemyUnits.Remove(enemyUnit))
            {
                board.RemoveUnit(cellAttacked);
                enemyUnit.Unsetup();
            }

            cellAttacked = null;
            CheckWinState();
        }

        private void CheckWinState()
        {
            List<UnitState> enemyUnits = GetEnemyUnits();

            if (enemyUnits.Count == 0)
            {
                onWinGameCallback?.Invoke();
            }
        }

        #endregion

        #region Turn methods

        public bool StartTurn(bool isPlayer1, Action onWinGameCallback)
        {
            if (IsPlayerTurn(isPlayer1))
            {
                this.onWinGameCallback = onWinGameCallback;

                if (!isPlayer1)
                {
                    basicAI.Play();
                }

                return true;
            }

            return false;
        }

        public bool EndTurn(bool isPlayer1)
        {
            if (IsPlayerTurn(isPlayer1))
            {
                SelectUnitCell(null);
                cellAttacked = null;

                List<UnitState> enemyUnits = GetEnemyUnits();
                ResetUnitsActionPoints(enemyUnits);

                isPlayer1Turn = !isPlayer1Turn;
                return true;
            }

            return false;
        }

        private void ResetUnitsActionPoints(List<UnitState> playerUnits)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                playerUnits[i].ResetActionPoints();
            }
        }

        #endregion

        #region Utils methods

        private bool IsPlayerTurn(bool isPlayer1)
        {
            return GameStarted && isPlayer1 == isPlayer1Turn;
        }

        private int GetCellUnitDistance(Cell cell)
        {
            return (cellUnitSelect && cell) ? board.GetCellsDistance(cellUnitSelect, cell) : int.MaxValue;
        }

        private List<UnitState> GetEnemyUnits()
        {
            return (isPlayer1Turn) ? player2Units : player1Units;
        }

        #endregion
    }
}
