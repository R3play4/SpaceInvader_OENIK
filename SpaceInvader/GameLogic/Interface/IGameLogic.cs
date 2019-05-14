// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ClassRepository;
    using ClassRepository.Model;

    /// <summary>
    /// GameLogic interface handles the communication with the GameModel layer.
    /// </summary>
    public interface IGameLogic
    {
        IGameModel Model { get; }
        /// <summary>
        /// Handles player movement.
        /// </summary>
        /// <param name="isMovingRight">true = right, false = left</param>
        void PlayerMove(bool? isMovingRight);

        /// <summary>
        /// Handles Player shooting logic.
        /// </summary>
        void PlayerShoot();

        /// <summary>
        /// Checks if the projectile hit any relevant GameItems
        /// </summary>
        /// <param name="projectile">prjectile that is examined</param>
        /// <param name="gameItem">gameItem that is examined</param>
        bool CollisionCheck(Projectile projectile, GameItem gameItem);

        /// <summary>
        /// Changes Game State
        /// </summary>
        /// <param name="newState">new State</param>
        void GameStateSwitch(GameState newState);

        /// <summary>
        /// Checks if the GameState is finnished
        /// </summary>
        /// <returns>true if the game GameState changed to Finnished </returns>
        bool GameEnd();

        /// <summary>
        /// Saves the games current state represented by the GameModel;
        /// </summary>
        /// <param name="fileName">name of the save file, XML format</param>
        void SaveGame(string fileName);

        /// <summary>
        /// Loads a desired state of the GameModel, that was saved earlier.
        /// </summary>
        /// <param name="fileName">name of the save file, XML format</param>
        void LoadGame(string fileName);
    }
}
