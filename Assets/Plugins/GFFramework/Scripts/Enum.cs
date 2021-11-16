using System;
using System.Collections.Generic;

namespace GFFramework.Enums
{
    /// <summary>
    /// I´m aware that I´m creating references to the game from the framework
    /// This will be auto-generated in the future using the names of the GameStates
    /// </summary>
    public enum GameStateKey
    {
        None,
        MainMenu,
        PlayerTurn,
        LoadGameScene,
        LoadMainMenuScene,
        LoadSession,
        EnemyTurn,
    }

    /// <summary>
    /// I´m aware that I´m creating references to the game from the framework
    /// This will be auto-generated in the future using the names of the scenes, 
    /// </summary>
    public enum SceneKey
    {
        None,
        MainMenu,
        Game
    }
}