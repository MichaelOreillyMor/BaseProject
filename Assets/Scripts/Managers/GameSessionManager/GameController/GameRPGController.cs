using GFF.SessionsMan.TurnBasedSessions;

using RPGGame.BoardCells;
using RPGGame.SessionsMan.Players;
using RPGGame.Units;

using System;

namespace RPGGame.SessionsMan
{
    /// <summary>
    /// Responsible  of the rules, actions and state of a PlayerRPG vs PlayerRPG match
    /// </summary>
    public class GameRPGController : ITurnBasedGameController
    {
        protected bool GameStarted { get; private set; }

        private IBoard board;

        private IPlayerRPG player1;
        private IPlayerRPG player2;

        private IUnitState unitSelect;
        private ICell cellUnitSelect;
        private ICell cellAttacked;

        private bool isPlayer1Turn;

        private Action onWinGameCallback;

        #region Setup methods

        public GameRPGController(IBoard board, IPlayerRPG player1, IPlayerRPG player2)
        {
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;
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
                player1.Unsetup();
                player2.Unsetup();
                GameStarted = false;
            }
        }

        #endregion

        #region Select cells methods

        public void OnSelectCell(ICell cell)
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
            IPlayerRPG enemyPlayer = GetEnemyPlayer();
  
            if (enemyPlayer.KillUnit(enemyUnit))
            {
                board.RemoveUnit(cellAttacked);
            }

            cellAttacked = null;

            if (enemyPlayer.IsDead())
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
                    IPlayerRPG player = GetTurnPlayer();
                    player.StartTurn();
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

                IPlayerRPG enemyPlayer = GetEnemyPlayer();
                enemyPlayer.ReseActionPoints();

                isPlayer1Turn = !isPlayer1Turn;
                return true;
            }

            return false;
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

        private IPlayerRPG GetEnemyPlayer()
        {
            return (isPlayer1Turn) ? player2 : player1;
        }

        private IPlayerRPG GetTurnPlayer()
        {
            return (isPlayer1Turn) ? player1 : player2;
        }

        #endregion
    }
}
