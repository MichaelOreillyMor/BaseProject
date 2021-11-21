using GFF.SessionsMan.TurnBasedSessions;

using RPGGame.BoardCells;
using RPGGame.SessionsMan.AI;
using RPGGame.Units;

using System;
using System.Collections.Generic;

namespace RPGGame.SessionsMan
{
    /// <summary>
    /// Responsible  of the rules, actions and state of a Player vs AI match
    /// </summary>
    public class PlayerVsAIController : ITurnBasedGameController
    {
        protected bool GameStarted { get; private set; }

        private IBoard board;
        private IPlayerAI playerAI;

        private List<IUnitState> player1Units;
        private List<IUnitState> player2Units;

        private IUnitState unitSelect;
        private ICell cellUnitSelect;
        private ICell cellAttacked;

        private bool isPlayer1Turn;

        private Action onWinGameCallback;

        #region Setup methods

        public PlayerVsAIController(IBoard board, IPlayerAI playerAI, List<IUnitState> player1Units, List<IUnitState> player2Units)
        {
            this.board = board;
            this.playerAI = playerAI;
            this.player1Units = player1Units;
            this.player2Units = player2Units;

            playerAI.SetOnSelectCallback(OnSelectCell);
            board.SetOnSelectCallback(OnSelectCell);
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

        private void UnsetupUnits(List<IUnitState> playerUnits)
        {
            for (int i = 0; i < playerUnits.Count; i++)
            {
                playerUnits[i].Unsetup();
            }
        }

        #endregion

        #region Select cells methods

        private void OnSelectCell(ICell cell)
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

        private void SelectUnitCell(ICell cell)
        {
            cellUnitSelect = cell;
            unitSelect = (cellUnitSelect != null) ? cellUnitSelect.GetUnit() : null;
        }

        #endregion

        #region Player actions methods

        private void TryMoveUnit(ICell cell)
        {
            if (cellUnitSelect != null && cell != null && !cell.HasUnit())
            {
                int distance = GetCellUnitDistance(cell);
                if (unitSelect.TryMovePosition(distance))
                {
                    board.MoveUnit(cellUnitSelect, cell);
                    SelectUnitCell(null);
                }
            }
        }

        private void TryAttackUnit(ICell cell)
        {
            if (cellUnitSelect != null && cell != null && cell.HasUnit())
            {
                IUnitState enemyUnit = cell.GetUnit();
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

        private void OnDeadUnit(IUnitState enemyUnit)
        {
            List<IUnitState> enemyUnits = GetEnemyUnits();
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
            List<IUnitState> enemyUnits = GetEnemyUnits();

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
                    playerAI.Play();
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

                List<IUnitState> enemyUnits = GetEnemyUnits();
                ResetUnitsActionPoints(enemyUnits);

                isPlayer1Turn = !isPlayer1Turn;
                return true;
            }

            return false;
        }

        private void ResetUnitsActionPoints(List<IUnitState> playerUnits)
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

        private int GetCellUnitDistance(ICell cell)
        {
            return (cellUnitSelect != null && cell != null) ? board.GetCellsDistance(cellUnitSelect, cell) : int.MaxValue;
        }

        private List<IUnitState> GetEnemyUnits()
        {
            return (isPlayer1Turn) ? player2Units : player1Units;
        }

        #endregion
    }
}
