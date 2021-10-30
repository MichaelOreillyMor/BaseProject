using UnityEngine;

namespace GFFramework
{
    /// <summary>
    /// This is an just an example, a mix between a saveGame, some references to prefabs, and game balance parameters
    /// </summary>
    public abstract class BaseGameData : ScriptableObject
    {
        protected BaseGameDataState gameDataState;

        public abstract BaseGameDataState GetGameDataState();
    }
}
