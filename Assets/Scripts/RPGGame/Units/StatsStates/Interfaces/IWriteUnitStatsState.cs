namespace RPGGame.Units.Stats
{
    /// <summary>
    /// Provides to the UnitState just the methods to change the state of the stats
    /// </summary>
    public interface IWriteUnitStatsState 
    {
        public void ForceDefeated();
        public void RemoveAllListeners();
        public bool ApplyAttackDamage(int damage);
        public bool TryAttack(int distance);
        public bool TryMove(int distance);
        public int GetAttackDamage();
        public void ResetActionPoints();
    }
}
