namespace RPGGame.Units.Stats
{
    public interface IUnitAction : IStatState
    {
        public int Cost { get; }
    }
}