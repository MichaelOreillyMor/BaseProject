using UnityEngine;

namespace RPGGame.GameDatas
{
    [CreateAssetMenu(menuName = "GameData/MapLevelData")]
    public class MapLevelData : ScriptableObject
    {
        public Vector2Int MapSize;
        public MapUnitPosition[] Player1Units;
        public MapUnitPosition[] Player2Units;
    }


    [System.Serializable]
    public class MapUnitPosition
    {
        public Vector2Int Position;
        public UnitData UnitData;
    }
}
