GFFramework is a framework (WIP) developed by Michael OReilly that is inspired by Unreal´s framework 
that works as a layer over the engine to speed up the games development process, it's still a WIP.

Is designed to load in the GameInitializer only the managers needed in a similar way to Unreal modules.
Only the GameStateManager or other implementation of IGameStateProvider is required by the GameInitializer.

All the parts of the framework are highly decouple, you don´t have to use the whole  framework. 
The GameStates are the glue that makes all the systems work, but no system (UIScreens,GameStates,Input...) is required by others.

1.---Definitions---

IProvider:
An Interface has been created for each Manager in order to be able to mockup the managers functionality and test different states of the game.
I also like to do this to avoid people from create public variables in the different managers and change their state directly, 
each manager (or any class) should be responsible of its state and has to avoid the use of public variables that are not readonly.

UIScreen:
A prefab that contains a Canvas and some UI components inside its hierarchy (e.g: UI for player´s inventory, HUD or Main menu)

GameState:
It changes the state of the game depending on player´s input and game flow (State pattern),
every state is resposible of the setup and unsetup of whatever is needed in that particular GameState.

SceneInfo:
It contains some information about the scene and have references to the UIScreens, PlayerController, 
or any other object that are part of the Scene and a list of the prefabs that have to be loaded from the pool.

2.---QA---

Why didn´t you use and IOC like Zenjet?

- I would like to implement it, but I didn't have time yet, I designed the framework over the idea of injecting dependencies in 
  initialization methods or constructors.

3.---GameStates and GameManagers---

GameStates system is the core of the game, it helps to:

-Create easily a game flow and to know the UI that needs to be display in each moment.

-Avoids heap memory fragmentation, the memory needed for the managers is allocated at the beggining of the game and released at the end, 
 memory needed for a particular game state is allocated (if needed) during its Setup() and released before changing to the next game state Unsetup().

4.---Scenes and SceneInfo---

Each scene can have one SceneInfo, 
It can have references to the UIScreens, PlayerController, or any other object that are part of the Scene and a list of the prefabs that have to be loaded from the pool.

This helps me to load these prefabs asynchronously with the scene and link their life the the scene´s life time.

Using a LoadSceneGameState the Scene is loaded and the references contained by SceneInfo are linked to the with the managers in charge of them.

The UIScreens life time is linked to the Scene life time, this is done like this to avoid the instantiation of UIScreens on run-time or 
having every single UIScreen loaded all the time.

You have also the option of load UIScreens on run-time if needed.
