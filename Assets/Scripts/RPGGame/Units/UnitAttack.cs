using RPGGame.GameDatas.Stats;

namespace RPGGame.Units.Stats
{
    public class UnitAttack : StatState, IUnitActionRange
    {
        public int Range { get; private set; }
        public int Cost { get; private set; }

        public UnitAttack(ActionRangeData actionData, float level) : base(actionData, level)
        {
            Range = actionData.Range;
            Cost = actionData.Cost;
        }

        public bool HasRange(int distance)
        {
            return (distance <= Range);
        }

        public bool HasCost(int actionPoints)
        {
            return (actionPoints >= Cost);
        }
    }
}
