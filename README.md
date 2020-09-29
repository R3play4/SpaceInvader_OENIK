# SpaceInvader_OENIK
## Team project for a WPF course.
###### How to Try
Open the SLN with Visual Studio -> Set "Display" project as Start Up project

###### Task Description 
Create the classic game, SpaceInvader. Player controls a Space Ship that can shoot projectiles. Player can hit 3 types of enemies. Each type worth a different amount of points. Player has 3 lifes. Upon the 3rd hit the game is over. Game reloades when the user killed all enemies. The game is gradually becoming faster. The game can be saved and loaded by the player

###### Structure 
The game is organized into 3 different layers.

1, Display layer. This layer displays the game using WPF and XAML. Each game item is represented by a Sprite.

2, Logic layer. Game rules are defined here. This layer have been unit tested.

3,Repository layer. This layer reperesnts all the data defines a game. The initial state is loaded from xml. (/SpaceInvader/Display/DefaultGameState/default.xml

###### Technologies 
WPF, XAML
