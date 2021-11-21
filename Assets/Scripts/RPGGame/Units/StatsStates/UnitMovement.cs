using RPGGame.GameDatasMan.Stats;

namespace RPGGame.Units.Stats
{    
     /// <summary>
     /// Current state of the Unit movement action and the cost of perform it per distance
     /// </summary>
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