GFFramework is a framework (WIP) developed by Michael OReilly that is inspired by Unreal´s framework.
This works as a layer over the engine to speed up the games development process, it's still a WIP.

It's designed to use the GameInitializer to load only the managers needed in a similar way to how Unreal modules or Unity packages work.
Only the GameStateManager or other implementation of IGameStateProvider is required by the GameInitializer.

All the parts of the framework are decouple, you don´t have to use the whole framework or all the managers. 
The GameStates are the glue that makes all the systems work, but no system/manager (UIScreens,GameStates,Input...) is required by others.

1.---Definitions---

IProvider:
An Interface has been created for each manager in order to be able to mockup the managers functionality and test different states of the game.
I also like to do this to avoid people from create public variables in the different managers and change their state directly, 
each manager (or any class, but specially managers) should be responsible of its state and has to avoid the use of public variables that are not readonly.

UIScreen:
A prefab that contains a Canvas and some UI components inside its hierarchy (e.g: UI for player´s inventory, HUD or Main menu)

GameState:
It changes the state of the game depending on player´s input and game flow (State pattern),
every state is resposible of the setup and unsetup of whatever is needed in that particular GameState.

SceneInfo:
It contains some information about the scene and has references to the UIScreens, PlayerController or any other objects 
that have been loaded by the Scene and a list of the prefabs that have to be loaded from the objects pool (WIP).

2.---QA---

->Why didn´t you use an IOC like Zenjet?

- I would like to implement it, but I didn't have time yet, I designed the framework over the idea of injecting dependencies in 
  initialization methods or constructors.

->You repeat the Setup() and Unsetup() methods in a lot of classes, why don´t you use an interface or abstract base class?

-The framework is a WIP I'm not sure of the parameters that those methods will need yet.
 In the GameStates the Setup() is the composition root where I inject the dependencies of the classes needed for that GameState.
 In the rest of the framework classes the Setup(/* some params */) is where that dependencies are injected.

3.---GameStates and GameManagers---

The GameStates system is the core of the game, it helps to:

-Create easily a game flow and to know the UI that needs to be display in each moment.

-Avoids heap memory fragmentation. The memory needed for the managers is allocated at the beggining of the game and released at the end, 
 resources needed for a particular GameState or scene are taken (if needed) during its Setup() and released (if needed) before changing 
 to the next GameState Unsetup(). This would work better in C++ without a GC, but the design is also useful in C#.

4.---Scenes and SceneInfo---

Each scene can have one SceneInfo prefab, 
It can have references to the UIScreens, PlayerCharacter, or any other objects that are part of the Scene and 
a list of the prefabs that have to be loaded from the pool.

This helps me to load these prefabs asynchronously with the scene and link their life time to the the scene´s life time.

Using a LoadSceneGameState the Scene is loaded and the references in SceneInfo are linked to the managers in charge of them.

The UIScreens life times are linked to the Scene life time, this is done like this to avoid the instantiation of UIScreens on run-time or 
having every single UIScreen loaded all the time.

In a rare case, you have also the option of load UIScreens on run-time if needed.
Right now I'm working on having also UIScreens that are persistent during all the game life time.

I´m also working on a CoroutinesManager as coroutines are something that everyone knows how to use, 
but I´m exploring also the option of using UniRx and reactive programming.