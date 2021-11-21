using RPGGame.GameDatasMan.Stats;

namespace RPGGame.Units.Stats
{
    public class UnitMovement : UnitAction
    {
        public UnitMovement(ActionData actionData, float level) : base(actionData, level)
        {

        }

        public int GetCost(int distance)
        {
            return (BaseCost * distance);
        }
    }
}