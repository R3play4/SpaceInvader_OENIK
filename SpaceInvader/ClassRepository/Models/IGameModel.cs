// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// GameModel Interface
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets the list UFOs
        /// </summary>
        List<UFO> UFOs { get; set; }

        /// <summary>
        /// Gets or sets the list of Projectiles
        /// </summary>
        List<Projectile> Projectiles { get; set; }

        /// <summary>
        /// Gets or sets the list of Shields
        /// </summary>
        List<Shield> Shields { get; set; }

        /// <summary>
        /// Gets or sets the Player
        /// </summary>
        Player Player { get; set; }

        /// <summary>
        /// Gets or sets the state of the Game
        /// </summary>
        GameState GameState { get; set; }

        /// <summary>
        /// Gets or sets the score of the game.
        /// </summary>
        int Score { get; set; }
    }
}
