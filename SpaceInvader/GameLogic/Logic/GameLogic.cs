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
        public IGameModel Model { get; set; }

        private IGameRepository repository;

        private Random r = new Random();

        private bool ufoMovingRight; // true = right, false = left

        public double UfoTimerTick { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">Game Model.</param>
        public GameLogic(string filePath)
        {
            this.repository = new GameRepository();
            this.Model = this.repository.LoadGameState(filePath);
            this.UfoTimerTick = 1000;
        }

        public GameLogic()
        {
            this.repository = new GameRepository();
            this.Model = null;
            this.UfoTimerTick = 1000;
        }

        /// <summary>
        /// 
        /// </summary>
        // public GameModel Model { get; }

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
            foreach (Projectile projectile in this.Model.Projectiles)
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
                this.Model.Score += ((UFO)gameItem).Points;
            }

            this.DeathCheck(projectile, typeof(Projectile));
        }

        public void DeathCheck(GameItem item, Type type)
        {
            if (item.HitPoint == 0)
            {
                if (type == typeof(Projectile))
                {
                    this.Model.Projectiles.Remove((Projectile)item);
                }
                else if (type == typeof(UFO))
                {
                    this.Model.UFOs.Remove((UFO)item);
                }
                else if (type == typeof(Shield))
                {
                    this.Model.Shields.Remove((Shield)item);
                }
                else if (type == typeof(Player))
                {
                    this.Model.GameState = GameState.Finished;
                }
            }
        }

        public void CleanupOffscreenProjectiles()
        {
            for (int i = this.Model.Projectiles.Count - 1; i >= 0; i--)
            {
                if (this.Model.Projectiles[i].Y < 0 || this.Model.Projectiles[i].Y > Settings.WindowHeight)
                {
                    this.Model.Projectiles.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Checks if the GameState is finnished
        /// </summary>
        /// <returns>true if the game GameState changed to Finnished </returns>
        public bool CheckGameEnd()
        {
            if (this.Model.Player.HitPoint < 1)
            {
                this.GameStateSwitch(GameState.Finished);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Changes Game State
        /// </summary>
        /// <param name="newState">new State</param>
        public void GameStateSwitch(GameState newState)
        {
            this.Model.GameState = newState;
        }

        public void UfoShoot()
        {
            if (r.Next(1,4) % 3 == 0)
            {
                this.Model.Projectiles.Add(this.Model.UFOs[r.Next() % this.Model.UFOs.Count].Shoot());
            }
        }

        public void PlayerMove(bool? direction)
        {
            if (direction == true && (Model.Player.X + Settings.ShipSize + Settings.PlayerStepSize) < Settings.WindowWidth)
            {
                this.Model.Player.Move(Settings.PlayerStepSize);
            }
            else if (direction == false && (Model.Player.X - Settings.PlayerStepSize) > 0)
            {
                this.Model.Player.Move(Settings.PlayerStepSize * -1);
            }
        }

        public bool CheckIfLevelCleared()
        {
            if (this.Model.UFOs.Count == 0)
            {
                GameModel tempModel = this.repository.LoadGameState(@"default.xml");
                this.Model.UFOs = tempModel.UFOs;
                this.Model.Projectiles = new List<Projectile>();
                this.UfoTimerTick *= 0.8;
                if (this.Model.Player.HitPoint < 6)
                {
                    this.Model.Player.HitPoint++;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads a desired state of the GameModel, that was saved earlier.
        /// </summary>
        /// <param name="fileName">name of the save file, XML format</param>
        public void LoadGame(string fileName)
        {
            this.Model = this.repository.LoadGameState(fileName);
        }

        public void UfoMove()
        {
            foreach (UFO ufo in this.Model.UFOs)
            {
                ufo.Move();
            }
        }

        public void UfoMoveSideways()
        {

            if (!IsSideMovingUFODisplayed()) // Checks if there's a sidemoving UFO already
            {
                // 20% chance that a sidemoving UFO will appear
                if (this.r.Next(1,100) > 90)
                {
                    // 50% chance of moving left or right.
                    if(this.r.Next(1,100) > 50)
                    {
                        UFO sideMovingUFO = new UFO(20, 25, 100);
                        this.ufoMovingRight = true;
                        Model.UFOs.Add(sideMovingUFO);
                    }
                    else
                    {
                        UFO sideMovingUFO = new UFO((int)(Settings.WindowWidth - 20), 25, 100);
                        this.ufoMovingRight = false;
                        Model.UFOs.Add(sideMovingUFO);
                    }
                }
            }
            else
            {
                // There is already a moving UFO. Loops through all the UFO's and moves the one with 100 points sideways.
                for (int i = 0; i < this.Model.UFOs.Count(); i++)
                {
                    UFO actualUFO = Model.UFOs[i];

                    if (actualUFO.Points == 100 && ufoMovingRight == true)
                    {
                        actualUFO.MoveSideWays(Settings.UfoSideStepSize);
                    }
                    else if (actualUFO.Points == 100 && ufoMovingRight == false)
                    {
                        actualUFO.MoveSideWays(Settings.UfoSideStepSize * -1);
                    }

                    if (actualUFO.X < 0 || actualUFO.X > Settings.WindowWidth)
                        Model.UFOs.Remove(actualUFO);
                }
            }
        }

        public bool IsSideMovingUFODisplayed()
        {
            int i = 0;

            while (i < Model.UFOs.Count() && Model.UFOs[i].Points != 100)
                i++;

            // true if displayed
            return i < Model.UFOs.Count();

        }

        /// <summary>
        /// Handles Player shooting logic.
        /// </summary>
        public void PlayerShoot()
        {
            this.Model.Projectiles.Add(this.Model.Player.Shoot());
        }

        /// <summary>
        /// Saves the games current state represented by the GameModel;
        /// </summary>
        /// <param name="fileName">name of the save file, XML format</param>
        public void SaveGame(string fileName)
        {
            this.repository.SaveGameState(fileName, (GameModel)this.Model);
        }
    }
}
