using GFFramework;

using UnityEngine;

namespace Game
{
    /// <summary>
    /// This is an just an example, a mix between a saveGame, some references to prefabs if needed, and game balance parameters
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
