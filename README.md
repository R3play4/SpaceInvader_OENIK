# SpaceInvader_OENIK
To Try: 
Open the SLN -> Set "Display" project as Start Up project

University Team project.

Task Description: Create the classic game, SpaceInvader. Player controls a Space Ship that can shoot projectiles. Player can hit 3 types of enemies. Each type worth a different type of points. Player has 3 lifes. Upon the 3rd hit the game is over. Game reloades when the user killed all enemies. The game is gradually becoming faster.

Structure: The game is organized into 3 different layers.

1, Display layer. This layer displays the game using WPF and XAML. Each game item is represented by a Sprite.

2, Logic layer. Game rules are defined here. This layer have been unit tested.

3,Repository layer. This layer reperesnts all the data defines a game. The initial state is loaded from xml. (/SpaceInvader/Display/DefaultGameState/default.xml

Technologies used: -WPF -XAML -Nunit
