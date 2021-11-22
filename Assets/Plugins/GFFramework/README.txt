GFFramework is a framework (WIP) developed by Michael OReilly that is inspired by Unreal´s framework.
This works as a layer over the engine to speed up the games development process, it's still a WIP.

It's designed to use the GameInitializer to load only the managers needed in a similar way to how Unreal modules or Unity packages work.
Only the GameStateManager or other implementation of IGameStateProvider is required by the GameInitializer.

All the parts of the framework are going to be decoupled, you don't have to use the whole framework or all the managers. 
The GameStates are the glue that makes all the systems work, but no system/manager (UIScreens,GameStates,Input...) 
is going to be required by others.

In the next weeks I´m going create an Assembly definition for each manager folder and complete the decoupling of the managers from each other.
Right now there are some dependencies between the UIManager and the GameState manager, 
I want to think about the dependecies between the Assembly definitions that I´m going to create carefully.

1.---Definitions---

IProvider:
An Interface has been created for each manager in order to be able to mockup the managers functionality.
I also like to do this to avoid people from create public variables in the different managers and change their state directly, 
each manager (or any class, but specially managers) should be responsible of its state and has to avoid the use of public variables 
that are not readonly.

UIScreen:
A prefab that contains a Canvas and some UI components inside its hierarchy (e.g: UI for player´s inventory, HUD or Main menu)

GameState:
It changes the state of the game depending on player´s input and game flow (State pattern),
every GameState is resposible of the setup and unsetup of whatever is needed in that particular GameState.

SceneUIScreenRefs:
It has references to the UIScreens that are part of the scene hierarchy, 
this UIScreens are created with the scene and have the same life-time.

2.---QA---

->Why didn´t you use an IOC like Zenjet?

- I would like to implement it, but I didn't have time yet, I designed the framework over the idea of injecting dependencies in 
  initialization methods or constructors.

->You repeat the Setup() and Unsetup() methods in a lot of classes, why don´t you use an interface or abstract base class?

-In the GameStates the Setup() is the composition root where I inject the dependencies of the classes needed for that GameState.
 In the rest of the classes the Setup(/* some params */) is where that dependencies are injected.

3.---GameStates and GameManagers---

The GameStates system is the core of the game, it helps to:

-Create easily a game flow that is easy to understand and if needed they can have a UIScreen associated (BaseUIGameState).

-Create a State pattern that is also very easy to understand.

-Every GameState is resposible of the setup and unsetup of whatever is needed in that particular GameState.

-In C++ this flow would avoid heap memory fragmentation. The memory needed for the managers is allocated 
 at the beggining of the game and released at the end, resources needed for a particular GameState or scene 
 are taken (if needed) during its Setup() and released (if needed) before changing to the next GameState Unsetup().

 -There is a special case, LoadSessionGameState, this game state uses factories to create all the resources needed for the match/play session,
  Onces that these resources are loaded and their dependencies resolved, it builds a GameController and gives it to the GameSessionManager.
  For now on the GameSessionManager is responsible of the GameController.

4.---LoadSceneGameState and SceneUIScreenRefs---

When LoadSceneGameState loads a new scene, checks if there is a SceneUIScreenRefs in the scene hierarchy.
Each scene can have one SceneUIScreenRefs prefab, it has references to the UIScreens that are part of the scene hierarchy.
This "pattern" helps to load prefabs asynchronously with the scene and link their life time to the the scene´s life time.

In a rare case, you have also the option of load UIScreens on run-time if needed.

5.---CoroutinesManager---

I created a CoroutinesManager to centralize the use of them and give to the GameStates (They are ScriptableObjects) the possibility of use them.
I´m exploring also the option of using UniRx and reactive programming.