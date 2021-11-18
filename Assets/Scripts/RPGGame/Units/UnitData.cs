using RPGGame.GameDatas.Stats;
using RPGGame.Units;

using UnityEngine;

namespace RPGGame.GameDatas
{
    [CreateAssetMenu(menuName = "GameData/UnitData")]
    public class UnitData : ScriptableObject
    {
        public UnitCosmeticController CosmeticPref;
        public UnitStatsData UnitStats;
    }
}