using RPGGame.Units;

namespace RPGGame.SessionsMan.Players
{
    public interface IPlayerRPG
    {
        bool IsDead();
        bool KillUnit(IUnitState unitState);
        void ReseActionPoints();
        void StartTurn();
        void Unsetup();
    }
}