using UnityEngine;

namespace RPGGame.GameDatas
{
    [CreateAssetMenu(menuName = "GameData/MapLevelData")]
    public class MapLevelData : ScriptableObject
    {
        public Vector2Int BoardSize;
        public MapUnitData[] Player1Units;
        public MapUnitData[] Player2Units;
    }


    [System.Serializable]
    public class MapUnitData
    {
        public Vector2Int Position;
        public UnitData UnitData;
        public float UnitLevel;
    }
}
