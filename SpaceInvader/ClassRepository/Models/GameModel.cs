// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Collections.Generic;
    using ClassRepository.Model;

    /// <summary>
    /// Represents the state of the game.
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// paused state
        /// </summary>
        Paused,

        /// <summary>
        /// running state
        /// </summary>
        Running,

        /// <summary>
        /// finnished state
        /// </summary>
        Finished
    }

    /// <summary>
    /// Represents the current state of the game at any momment.
    /// </summary>
    public class GameModel : IGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        public GameModel()
        {
            this.Player = new Player(350, 200);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        /// <param name="ufos">ufo list</param>
        /// <param name="projectiles"> projectile list</param>
        /// <param name="shields">shields list</param>
        /// <param name="player">player</param>
        /// <param name="gamestate">state of the game</param>
        /// <param name="score">score acchieved by the palyer</param>
        public GameModel(List<UFO> ufos, List<Projectile> projectiles, List<Shield> shields, Player player, GameState gamestate = GameState.Paused, int score = 0)
        {
            this.UFOs = ufos;
            this.Projectiles = projectiles;
            this.Shields = shields;
            this.Player = player;
            this.GameState = gamestate;
            this.Score = score;
        }

        /// <summary>
        /// Gets or sets the list UFOs
        /// </summary>
        public List<UFO> UFOs { get; set; }

        /// <summary>
        /// Gets or sets the list of Projectiles
        /// </summary>
        public List<Projectile> Projectiles { get; set; }

        /// <summary>
        /// Gets or sets the list of Shields
        /// </summary>
        public List<Shield> Shields { get; set; }

        /// <summary>
        /// Gets or sets the Player
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the state of the Game
        /// </summary>
        public GameState GameState { get; set; }

        /// <summary>
        /// Gets or sets the score of the game.
        /// </summary>
        public int Score { get; set; }
    }
}
