using GFFramework;
using GFFramework.GameDatas;

using UnityEngine;

namespace Game.GameDatas
{
    // This is an just an example, a mix between a saveGame and game balance parameters list, in a real this could be loaded from a JSON
    /// <summary>
    /// The initial state of game variables, e.g: max life amount 
    /// </summary>
    [CreateAssetMenu(menuName = "GameData")]
    public class GameData : BaseGameData
    {
        public override BaseGameDataState GetGameDataState()
        {
            if (gameDataState != null)
            {
                gameDataState = new GameDataState(this);
            }

            return gameDataState;
        }
    }
}
