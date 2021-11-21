using RPGGame.GameDatasMan.Stats;

namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Current state of the Unit attack action and the cost of perform it
    /// </summary>
    public class UnitAttack : UnitAction, IUnitActionRange
    {
        public int Range { get; private set; }

        public UnitAttack(ActionRangeData actionData, float level) : base(actionData, level)
        {
            Range = actionData.Range;
        }

        public bool HasRange(int distance)
        {
            return (distance <= Range);
        }

        public int GetCost()
        {
            return BaseCost;
        }
    }
}
