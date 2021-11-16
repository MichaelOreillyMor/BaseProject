using UnityEngine;

namespace RPGGame.GameDatas
{
    [CreateAssetMenu(menuName = "GameData/UnitData")]
    public class UnitData : ScriptableObject
    {
        public GameObject CosmeticPref;
        public UnitStats unitStats;
    }

    [System.Serializable]
    public class UnitStats
    {
        public float HealthPoints;
        public float AttackDamage;
        public float AttackRange;
        public float MoveRange;
    }
}