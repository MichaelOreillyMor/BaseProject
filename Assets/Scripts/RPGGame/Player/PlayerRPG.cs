using RPGGame.SessionsMan.Players.AIs;
using RPGGame.Units;

using System.Collections.Generic;


namespace RPGGame.SessionsMan.Players
{
    /// <summary>
    /// Represents a player of the RPG game, it can have an AI
    /// </summary>
    public class PlayerRPG : IPlayerRPG
    {
        private List<IUnitState> unitStates;
        private IAIController AIController;

        #region Setup/Unsetup methods

        public PlayerRPG(List<IUnitState> unitStates, IAIController AIController)
        {
            this.unitStates = unitStates;
            this.AIController = AIController;
        }

        public void Unsetup()
        {
            for (int i = 0; i < unitStates.Count; i++)
            {
                unitStates[i].Unsetup();
            }
        }

        #endregion

        #region Turn methods

        public void StartTurn()
        {
            AIController?.Play();
        }

        public void ReseActionPoints()
        {
            for (int i = 0; i < unitStates.Count; i++)
            {
                unitStates[i].ResetActionPoints();
            }
        }

        #endregion

        #region Dead units methods

        public bool KillUnit(IUnitState unitState)
        {
            if (unitStates.Remove(unitState))
            {
                unitState.Unsetup();
                return true;
            }

            return false;
        }

        public bool IsDead()
        {
            return (unitStates.Count == 0);
        }

        #endregion
    }
}