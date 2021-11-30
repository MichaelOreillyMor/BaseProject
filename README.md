# 1 My Framework

This game is built over GFFramework, my own framework that I have been developing for
the past few weeks. It works as a layer over Unity to speed up the games development
process, it's still a work in progress.

It's designed to use the GameInitializer to load only the managers needed in a similar way to
how Unreal modules work. Only the GameStateManager or other implementation of
IGameStateProvider is required by the GameInitializer.

All the parts of the framework are going to be decoupled, you don't have to use the whole
framework or all the managers. The GameStates are the glue that makes all the systems
work, but no manager (UIMan ,InputMan, CameraMan...) is going to be required by others.

Right now there are some dependencies between the UIManager and the GameState
manager. In the next few weeks, I'm going to create an Assembly definition for each
manager folder and complete the decoupling of the managers from each other.

## 1. 1 GameStates system

The game’s flow is a State pattern. Each state of the game is a ScriptableObject, which
helps to create as many states as you want easily. Each GameState has this important methods:

**Setup():** Entry method where the dependencies of theGameState components are resolved
(Dependency Injection Composition root), the components can also start to listen to the
events that they need here.

**Unsetup():** Exit method where all the resources takenby the game state are disposed and
stop listening to events.

**GameStateManager** is responsible for loading and unloading each GameState.

**BaseUIGameState** is an important GameState that is worth mentioning. It's the base class
that gets a UIScreen (Prefab [Canvas +UI components]) from the UIManager and shows it
to the player.

![PlayerTurn](/readmeImgs/PlayerTurn.png)

# 2 The Game

## 2. 1 The Game Initialization

**LoadSceneGameState**

Loads a Scene, sends the UIScreens that are part of this scene to the UIManager and
moves to the next GameState.

**LoadSessionGameState**

Builds the play session/game match. It gets a MapLevelData and creates:

- The Board and its Cells using a MapBoardFactory.
- The UnitStates(soldiers and monsters) using a UnitsBoardFactory.
- The PlayerController sthat handles the state of a player(e.g: soldiers alive).
- The GameController that handles the match state and which player wins.

The Factories use the PoolManager to get the cells, UnitStates, UnitConsmetics and
UIPanelUnitStats. Then everything is injected, wired and returned.

![pawnFactory](/readmeImgs/pawnFactory.png)

the GameController now is ready and it’s given to the SessionManager that starts the
game session. **After this point of the flow, all the references to these objects 
are Interfaces, these Interfaces provide the methods needed in each context.
e.g: Cell.Setup() is not visible for classes that use ICell.**


## 2. 2 The Game Loop

**TurnBasedSessionManager**

Acts as connection point between the players and the GameController and provides some
utilities like pause the game.

![SessionManager](/readmeImgs/SessionManager.png)

**GameRPGController**

Responsible for the rules, actions, who wins and state of a PlayerRPG vs PlayerRPG match.
It handles the interactions between the PlayerRPG UnitStates and the Board.

![GameController](/readmeImgs/GameController.png)

## 2. 3 game Board and Cells

The Board provide utilities to remove and add IUnitState from the Cells.

![Cell](/readmeImgs/Cell.png)

## 2. 3 UnitStates

Current state of a Unit (e.g: Soldier, Monster), it acts as a facade (pattern) to connect the
Cosmetic, Transform and UnitStatsState (life, attack, actionPoints...)

![UnitState](/readmeImgs/UnitState.png)

## 2. 4 UnitStatStates

Current state of the Unit stats and actions. Their initial values are loaded from a UnitStatsData.

![StatsState](/readmeImgs/StatsState.png)

**IReadUnitStatsState**

Provides to the UI (or any other listeners) only methods to listen to stats changes.

**IWriteUnitStatsState**

Provides to the UnitState just the methods to
change the state of the stats (e.g: receive an attack).

**UnitStatsData**

The UnitStatsData is obtained from the list of UnitPositions 
of each Player in the MapLevelData.
![MapLevelData](/readmeImgs/MapLevelData.png)

**UnitActions**

The health and the ActionPoints of each unit are plain StatStates, the attack and movement
of the units on the other hand are UnitAction : StatStates since they have a value and 
a cost to perform them.

![UnitAttack](/readmeImgs/UnitAttack.png)
![UnitMove](/readmeImgs/UnitMove.png)

## 2. 5  Game Dependencies 

- UnitStatStates and UnitActionStates initial values are obtained from serialized datas
- A StatsState has UnitStatStates and UnitActionStates.
- A UnitCosmetic has a 3D model inside its hierarchy and an AnimatorController.
- A UnitState has an IUnitCosmetic and IStatsState.
- A PlayerRPG has N IUnitStates and can have an IAIController.
- A Board has N ICells.
- A GameRPGController has an IBoard and two IPlayerRPG.
- A GameSessionManager has a IGameRPGController.
- A PlayerTurnStates interacts with the GameSessionManager.
- GameSessionManager decides the state of the game, that can be validated locally or in a server.

RPGGame objects are loaded using theMapLevelData that contains the UnitPositionDatas and Board size. 
All the Datas are ScriptableObjects or serialize classes inside them. 
The references to RPGGame objects are Interfaces, eveything is testable and encapsulated.

## 2. 6  GameStates Flow
![Game flow](/readmeImgs/GameFlow.png)