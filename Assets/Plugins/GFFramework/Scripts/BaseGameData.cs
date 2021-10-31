using UnityEngine;

namespace GFFramework.GameDatas
{
    // This is an just an example, a mix between a saveGame and game balance parameters list, in a real this could be loaded from a JSON
    /// <summary>
    /// The initial state of game variables, e.g: max life amount 
    /// </summary>
    public abstract class BaseGameData : ScriptableObject
    {
        protected BaseGameDataState gameDataState;

        public abstract BaseGameDataState GetGameDataState();
    }
}
