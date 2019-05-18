// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpaceInvaderLogic
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
    using global::SpaceInvaderLogic.Interfaces;
    using GlobalSettings;

    /// <summary>
    /// Handles the Game's logic. Implements IGameLogic interface
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private Random r = new Random();
        private bool ufoMovingRight; // true = right, false = left
        private IGameRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="filePath">path of the file that will be loaded</param>
        public GameLogic(string filePath)
        {
            this.repository = new GameRepository();
            this.Model = this.repository.LoadGameState(filePath);
            this.UfoTimerTick = 1000;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        public GameLogic()
        {
            this.repository = new GameRepository();
            this.Model = null;
            this.UfoTimerTick = 1000;
        }

        /// <summary>
        /// Gets or sets GameModel interface
        /// </summary>
        public IGameModel Model { get; set; }

        /// <summary>
        /// Gets or sets UfoTimer
        /// </summary>
        public double UfoTimerTick { get; set; }

        /// <summary>
        /// Checks if the projectile hit any relevant GameItems
        /// </summary>
        /// <param name="projectile">prjectile that is examined</param>
        /// <param name="gameItem">item that shot the projectile</param>
        /// <returns>true if coliission occured, fale otherwise</returns>
        public bool CollisionCheck(Projectile projectile, GameItem gameItem)
        {
            return Geometry.Combine(projectile.Shape(), gameItem.Shape(), GeometryCombineMode.Intersect, null).GetArea() > 0;
        }

        /// <summary>
        /// Moves all of the projectiles
        /// </summary>
        public void ProjectileMove()
        {
            foreach (Projectile projectile in this.Model.Projectiles)
            {
                projectile.Move();
            }
        }

        /// <summary>
        /// Handles the collision. Addes points to player if necessary. Removes dead objects.
        /// </summary>
        /// <param name="projectile">projectile to be checked</param>
        /// <param name="gameItem">itme that collided with the projectile</param>
        public void HandleCollision(Projectile projectile, GameItem gameItem)
        {
            projectile.TakeDamage();
            gameItem.TakeDamage();
            if (projectile.SourceObject.GetType() == typeof(Player) && gameItem.GetType() != typeof(Shield))
            {
                this.Model.Score += ((Ufo)gameItem).Points;
            }

            this.DeathCheck(projectile, typeof(Projectile));
        }

        /// <summary>
        /// Checks if the hit point of  gameItem reached zero. If yes than removes it from the proper list.
        /// </summary>
        /// <param name="item">item to be checked</param>
        /// <param name="type">type of the item</param>
        public void DeathCheck(GameItem item, Type type)
        {
            if (item.HitPoint == 0)
            {
                if (type == typeof(Projectile))
                {
                    this.Model.Projectiles.Remove((Projectile)item);
                }
                else if (type == typeof(Ufo))
                {
                    this.Model.Ufos.Remove((Ufo)item);
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

        /// <summary>
        /// Cleans up the projectiles that have left the screen
        /// </summary>
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

        /// <summary>
        /// Creates projectile at the UFO's x y position and adds it to the projectile list.
        /// </summary>
        public void UfoShoot()
        {
            if (this.Model.Ufos.Count > 0 && this.r.Next(1, 4) % 3 == 0)
            {
                if (this.Model.Ufos.Count() != 0)
                {
                    this.Model.Projectiles.Add(this.Model.Ufos[this.r.Next() % this.Model.Ufos.Count].Shoot());
                }
            }
        }

        /// <summary>
        /// Moves player left or right. Checks if the player have reached the side of the window.
        /// </summary>
        /// <param name="isMovingRight">true = right, false = left</param>
        public void PlayerMove(bool? isMovingRight)
        {
            if (isMovingRight == true && (this.Model.Player.X + Settings.ShipSize + Settings.PlayerStepSize) < Settings.WindowWidth)
            {
                this.Model.Player.Move(Settings.PlayerStepSize);
            }
            else if (isMovingRight == false && (this.Model.Player.X - Settings.PlayerStepSize) > 0)
            {
                this.Model.Player.Move(Settings.PlayerStepSize * -1);
            }
        }

        /// <summary>
        /// Checks if any UFO have left on screen. Acts extra life to the player if cleared. Makes the UFO faster.
        /// </summary>
        /// <returns>true if there are no more ufos</returns>
        public bool CheckIfLevelCleared()
        {
            if (this.Model.Ufos.Count == 0)
            {
                GameModel tempModel = this.repository.LoadGameState("..\\..\\DefaultGameState\\default.xml");
                this.Model.Ufos = tempModel.Ufos;
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

        /// <summary>
        /// Moves the ufo object
        /// </summary>
        public void UfoMove()
        {
            foreach (Ufo ufo in this.Model.Ufos)
            {
                if (ufo.Points != 100)
                {
                    ufo.Move();
                }
            }
        }

        /// <summary>
        /// Moves the UFO sideways.
        /// </summary>
        public void UfoMoveSideways()
        {
            if (!this.IsSideMovingUfoDisplayed())
            {
                // 20% chance that a sidemoving UFO will appear
                if (this.r.Next(1, 100) > 90)
                {
                    // 50% chance of moving left or right.
                    if (this.r.Next(1, 100) > 50)
                    {
                        Ufo sideMovingUFO = new Ufo(20, 25, 100);
                        this.ufoMovingRight = true;
                        this.Model.Ufos.Add(sideMovingUFO);
                    }
                    else
                    {
                        Ufo sideMovingUFO = new Ufo((int)(Settings.WindowWidth - 20), 25, 100);
                        this.ufoMovingRight = false;
                        this.Model.Ufos.Add(sideMovingUFO);
                    }
                }
            }
            else
            {
                // There is already a moving UFO. Loops through all the UFO's and moves the one with 100 points sideways.
                for (int i = 0; i < this.Model.Ufos.Count(); i++)
                {
                    Ufo actualUFO = this.Model.Ufos[i];

                    if (actualUFO.Points == 100 && this.ufoMovingRight == true)
                    {
                        actualUFO.MoveSideWays(Settings.UfoSideStepSize);
                    }
                    else if (actualUFO.Points == 100 && this.ufoMovingRight == false)
                    {
                        actualUFO.MoveSideWays(-Settings.UfoSideStepSize);
                    }

                    if (actualUFO.X < 0 || actualUFO.X > Settings.WindowWidth)
                    {
                        this.Model.Ufos.Remove(actualUFO);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if there is any SideMoving ufos on the screen. (Sidemocing UFO is identified by the fact that they worth 100 points
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsSideMovingUfoDisplayed()
        {
            int i = 0;

            while (i < this.Model.Ufos.Count() && this.Model.Ufos[i].Points != 100)
            {
                i++;
            }

            // true if displayed
            return i < this.Model.Ufos.Count();
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
