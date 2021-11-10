using UnityEngine;

namespace GFFramework.GameDatas
{
    /// <summary>
    /// WIP This is an just an example, a mix between a saveGame and game balance parameters list, in a real this could be loaded from a JSON
    /// It contains the initial state of game variables (BaseGameData), e.g: max life = 10 and works as a factory to build a class 
    /// that contains their changes during this game session and events to listen to these changes.
    /// </summary>
    public abstract class BaseGameData : ScriptableObject
    {
        protected BaseGameDataState gameDataState;

        public abstract BaseGameDataState GetGameDataState();
    }
}
