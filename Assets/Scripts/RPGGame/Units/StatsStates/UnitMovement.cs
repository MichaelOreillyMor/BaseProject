using RPGGame.GameDatas.Stats;

namespace RPGGame.Units.Stats
{
    public class UnitMovement : UnitAction
    {
        public UnitMovement(ActionData actionData, float level) : base(actionData, level)
        {

        }

        public bool HasCost(int distance, int actionPoints)
        {
            int distanceCost = (Cost * distance);
            return (actionPoints >= distanceCost);
        }
    }
}