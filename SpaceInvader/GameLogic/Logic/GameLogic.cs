// <copyright file="GameLogic.cs" company="PlaceholderCompany">
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
    using System.Windows.Media;
    using ClassRepository;
    using ClassRepository.Model;
    using ClassRepository.Repository;
    using global::GameLogic.Interface;
    using GlobalSettings;

    /// <summary>
    /// Handles the Game's logic. Implements IGameLogic interface
    /// </summary>
    public class GameLogic : IGameLogic
    {
        public IGameModel model;

        private IGameRepository repository;

        private Random r = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        public GameLogic(IGameModel model)
        {
            this.model = model;
            this.repository = new GameRepository();
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
        public bool CollisionCheck(Projectile projectile, GameItem gameItem)
        {
            return Geometry.Combine(projectile.Shape(), gameItem.Shape(), GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

        public void ProjectileMove()
        {
            foreach (Projectile projectile in this.model.Projectiles)
            {
                projectile.Move();
            }
        }

        public void HandleCollision(Projectile projectile, GameItem gameItem)
        {
            projectile.TakeDamage();
            gameItem.TakeDamage();
            if (projectile.SourceObject.GetType() == typeof(Player) && gameItem.GetType() != typeof(Shield))
            {
                this.model.Score += ((UFO)gameItem).Points;
            }

            this.DeathCheck(projectile, typeof(Projectile));
        }

        public void DeathCheck(GameItem item, Type type)
        {
            if (item.HitPoint == 0)
            {
                if (type == typeof(Projectile))
                {
                    this.model.Projectiles.Remove((Projectile)item);
                }
                else if (type == typeof(UFO))
                {
                    this.model.UFOs.Remove((UFO)item);
                }
                else if (type == typeof(Shield))
                {
                    this.model.Shields.Remove((Shield)item);
                }
                else if (type == typeof(Player))
                {
                    this.model.GameState = GameState.Finished;
                }
            }
        }

        public void CleanupOffscreenProjectiles()
        {
            for (int i = this.model.Projectiles.Count - 1; i >= 0; i--)
            {
                if (this.model.Projectiles[i].Y < 0 || this.model.Projectiles[i].Y > Settings.WindowHeight)
                {
                    this.model.Projectiles.RemoveAt(i);
                }
            }
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

        public void UfoShoot()
        {
            if (r.Next(1,4) % 3 == 0)
            {
                this.model.Projectiles.Add(this.model.UFOs[r.Next() % this.model.UFOs.Count].Shoot());
            }
        }

        public void PlayerMove(bool? direction)
        {
            if (direction == true && (model.Player.X + Settings.ShipSize + Settings.PlayerStepSize) < Settings.WindowWidth)
            {
                this.model.Player.Move(Settings.PlayerStepSize);
            }
            else if (direction == false && (model.Player.X - Settings.PlayerStepSize) > 0)
            {
                this.model.Player.Move(Settings.PlayerStepSize * -1);
            }
        }

        /// <summary>
        /// Loads a desired state of the GameModel, that was saved earlier.
        /// </summary>
        /// <param name="fileName">name of the save file, XML format</param>
        public void LoadGame(string fileName)
        {
            this.model = this.repository.LoadGameState(fileName);
        }

        public void UfoMove()
        {
            //if (!IsSideMovingUFODisplayed())
            //{
            //    UFO sideMovingUFO = new UFO(20, 40, 100);
            //    model.UFOs.Add(sideMovingUFO);
            //}
            foreach (UFO ufo in this.model.UFOs)
            {
                ufo.Move();
            }


        }

        public void UfoMoveSideways()
        {
            if (!IsSideMovingUFODisplayed())
            {
                UFO sideMovingUFO = new UFO(20, 40, 100);
                model.UFOs.Add(sideMovingUFO);
            }
            else
            {
                foreach (UFO ufo in this.model.UFOs)
                {
                    if (ufo.Points == 100)
                        ufo.MoveSideWays();
                }
            }
        }

        public bool IsSideMovingUFODisplayed()
        {
            // kell egy fix y amit viszgálni lehet
            int i = 0;

            while (i < model.UFOs.Count() && model.UFOs[i].Points != 100)
                i++;

            // true if displayed
            return i < model.UFOs.Count();

        }

        /// <summary>
        /// Handles Player shooting logic.
        /// </summary>
        public void PlayerShoot()
        {
            this.model.Projectiles.Add(this.model.Player.Shoot());
        }

        /// <summary>
        /// Saves the games current state represented by the GameModel;
        /// </summary>
        /// <param name="fileName">name of the save file, XML format</param>
        public void SaveGame(string fileName)
        {
            this.repository.SaveGameState(fileName, (GameModel)this.model);
        }
    }
}
