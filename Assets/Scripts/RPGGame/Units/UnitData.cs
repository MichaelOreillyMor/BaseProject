using RPGGame.GameDatas.Stats;
using RPGGame.Units;

using UnityEngine;

namespace RPGGame.GameDatas
{
    [CreateAssetMenu(menuName = "GameData/UnitData")]
    public class UnitData : ScriptableObject
    {
        public UnitCosmetic CosmeticPref;
        public UnitStatsData UnitStats;
    }
}