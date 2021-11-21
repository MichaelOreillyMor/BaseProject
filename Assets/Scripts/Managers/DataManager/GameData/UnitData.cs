using RPGGame.GameDatasMan.Stats;
using RPGGame.Units;

using UnityEngine;

namespace RPGGame.DatasMan.GameDatas
{
    [CreateAssetMenu(menuName = "GameData/UnitData")]
    public class UnitData : ScriptableObject
    {
        public UnitCosmetic CosmeticPref;
        public UnitStatsData UnitStats;
    }
}