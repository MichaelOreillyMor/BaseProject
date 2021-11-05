GFFramework is a framework (WIP) developed by Michael OReilly that is inspired by Unreal´s framework 
that works as a layer over the engine to speed up the games development process.

Is designed to load in the GameInitializer only the managers needed in a similar way to Unreal modules.

The core of the game is the GameStates system (State pattern) that changes the state of the game depending on the player´s input and game flow,
every state is resposible of the setup and unsetup of whatever is needed in that particular GameState.

GameStates system helps to:

-Create easily a game flow and to know the UI that needs to be display in each moment.

-Avoids heap memory fragmentation, the memory needed for the managers is allocated at the beggining of the game and released at the end, 
 memory needed for a particular game state is allocated (if needed) during its Setup() and released before changing to the next game state Unsetup().

An Interface (Provider) has been created for each Manager in order to be able to mockup the managers and test different states of the game.
I also like to do this to avoid people from create public variables in the different managers and change their state directly, 
each manager should be responsible of its state and has to avoid the use of public variables.
