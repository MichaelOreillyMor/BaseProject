GFFramework is a framework developed by Michael OReilly that is inspired by Unreal´s framework 
that works as a layer over the engine to speed up the games development.

Is designed to load in the GameInitializer only the managers needed in a similar way to Unreal modules.

The core of the game is the GameStates system that changes the state of the game depending on the player´s input and game flow,
every state is resposible of the setup and unsetup of whatever is needed in that particular GameState.

GameStates system helps to create easily a game flow and know the UI that needs to be display in each moment.

An Interface (Provider) has been created for each Manager in order to be able to mockup the managers and test different states of the game.
I also like to do this to avoid people from create public variables in the different managers and change the state of them, 
each manager should be responsible of its state and avoid the use of public variables.