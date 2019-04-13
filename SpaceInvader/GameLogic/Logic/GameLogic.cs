﻿// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ClassRepository;
    using global::GameLogic.Interface;

    /// <summary>
    /// Handles the Game's logic. Implements IGameLogic interface
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private GameModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        public GameLogic(GameModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// 
        /// </summary>
        // public GameModel Model { get; }

        /// <summary>
        /// Gets or sets threads that will handle the different GameItems.
        /// </summary>
        public Thread[] Threads { get; set; }

        /// <summary>
        /// Checks if the projectile hit any relevant GameItems
        /// </summary>
        /// <param name="projectile">prjectile that is examined</param>
        public void CollisionCheck(Projectile projectile)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the GameState is finnished
        /// </summary>
        /// <returns>true if the game GameState changed to Finnished </returns>
        public bool GameEnd()
        {
            return this.model.GameState == GameState.Finished;
        }

        /// <summary>
        /// Changes Game State
        /// </summary>
        /// <param name="newState">new State</param>
        public void GameStateSwitch(GameState newState)
        {
            this.model.GameState = newState;
        }

        /// <summary>
        /// Loads a desired state of the GameModel, that was saved earlier.
        /// </summary>
        public void LoadGame()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles player movement.
        /// </summary>
        /// <param name="direction">true = right, false = left</param>
        public void PlayerMove(bool direction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles Player shooting logic.
        /// </summary>
        public void PlayerShoot()
        {
            Projectile projectile = new Projectile(this.model.Player.y, this.model.Player.x, true, this.model.Player);
            this.model.Projectiles.Add(projectile);
        }

        /// <summary>
        /// Saves the games current state represented by the GameModel;
        /// </summary>
        public void SaveGame()
        {
            throw new NotImplementedException();
        }
    }
}
