using GFFramework.GameSession;

using RPGGame.BoardCells;
using RPGGame.GameSession.AI;
using RPGGame.Units;

using System;
using System.Collections.Generic;

namespace RPGGame.GameSession
{
    public class RPGGameSessionManager : GameSessionManager, IRPGGameSessionProvider
    {
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

        public void InitSession(Board board, List<UnitState> player1Units, List<UnitState> player2Units) 
        {
            if (TryInitSession())
            {
                this.board = board;
                this.player1Units = player1Units;
                this.player2Units = player2Units;

                board.SetOnSelectCallback(OnSelectCell);
                isPlayer1Turn = true;

                basicAI = new BasicAI(board, player2Units.Count, false, OnSelectCell);
            }
        }

        #endregion

        #region Unsetup methods

        public override void OnPreEndSession()
        {
            board.Unsetup();
            UnsetupUnits(player1Units);
            UnsetupUnits(player2Units);
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

        #region Player turn methods

        public void StartTurn(Action onWinGameCallback)
        {
            if (GameStarted)
            {
                this.onWinGameCallback = onWinGameCallback;
            }
        }

        public bool EndTurn(bool isPlayer1)
        {
            if (GameStarted && isPlayer1 == isPlayer1Turn)
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

        private int GetCellUnitDistance(Cell cell)
        {
            return (cellUnitSelect && cell) ? board.GetCellsDistance(cellUnitSelect, cell) : int.MaxValue;
        }

        private List<UnitState> GetEnemyUnits()
        {
            return (isPlayer1Turn) ? player2Units : player1Units;
        }

        public void PlayAI()
        {
            if (GameStarted)
            {
                basicAI.Play();
            }
        }
    }
}
