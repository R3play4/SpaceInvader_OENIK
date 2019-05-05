// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Collections.Generic;
    using ClassRepository.Model;

    public enum GameState
    {
        Paused,
        Running,
        Finished
    }

    public class GameModel : IGameModel
    {
        public GameModel()
        {
        }

        public GameModel(List<UFO> ufos, List<Projectile> projectiles, List<Shield> shields, Player player, GameState gamestate = GameState.Paused, int score = 0)
        {
            this.UFOs = ufos;
            this.Projectiles = projectiles;
            this.Shields = shields;
            this.Player = player;
            this.GameState = gamestate;
            this.Score = score;
        }

        public List<UFO> UFOs { get; set; }

        public List<Projectile> Projectiles { get; set; }

        public List<Shield> Shields { get; set; }

        public Player Player { get; set; }

        public GameState GameState { get; set; }

        public int Score { get; set; }
    }
}
