using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceInvader.Interfaces
{
    public interface IGameLogic
    {
        // This will handle the connection to the GameModel Layer
        // IGameModel model{ get; set;};
         
        // Responsible for handling multiple GameObjects
        Thread[] Threads { get; set; }

        // Method handling Player movements. True = Right. False = Left
        void PlayerMove(bool direction);

        // Method handling Player shooting.
        void PlayerShoot();

        // Checks if the bullet hit another target
        void CollisionCheck(/*Projectile projectile*/);

        // Switches the GameState
        void GameStateSwitch();

        // Checks if the GameState is 'Finnished'
        void GameEnd();

        // Save Game. Saves the actual State of the game.
        void SaveGame();

        // Loads a saved state of the game
        void LoadGame();

    }
}
