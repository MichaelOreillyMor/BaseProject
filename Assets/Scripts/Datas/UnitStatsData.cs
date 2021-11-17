using UnityEngine;

namespace RPGGame.GameDatas.Stats
{
    [System.Serializable]
    public class UnitStatsData
    {
        public StatData ActionPoints;
        public StatData Health;
        public ActionData Move;
        public ActionRangeData Attack;
    }

    [System.Serializable]
    public class StatData
    {
        public int BaseValue;
        public float ScalePerLevel;
    }

    [System.Serializable]
    public class ActionData : StatData
    {
        public int Cost;
    }

    [System.Serializable]
    public class ActionRangeData : StatData
    {
        public int Range;
        public int Cost;
    }
}