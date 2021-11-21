using GFF.DatasMan.GameDatas;

using UnityEngine;

namespace RPGGame.DatasMan.GameDatas
{
    // This is an just an example, a mix between a saveGame and other datas needed for the game, in a real this could be loaded from a JSON
    /// <summary>
    /// The initial state of game variables, e.g: max life amount 
    /// </summary>
    [CreateAssetMenu(menuName = "GameData/GameData")]
    public class GameData : BaseGameData
    {
        public MapLevelData[] MapLevelDatas;
    }
}
