using RPGGame.GameDatas.Stats;

namespace RPGGame.Units.Stats
{
    public class UnitMovement : StatState, IUnitAction
    {
        public int Cost { get; private set; }

        public UnitMovement(ActionData actionData, float level) : base(actionData, level)
        {
            Cost = actionData.Cost;
        }

        public bool HasCost(int distance, int actionPoints)
        {
            int distanceCost = (Cost * distance);
            return (actionPoints >= distanceCost);
        }
    }
}
