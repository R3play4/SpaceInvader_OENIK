// <copyright file="GameLogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpaceInvaderLogic.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media;
    using ClassRepository;
    using ClassRepository.Model;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Unit Testing for GameLogic using mocked GameModel.
    /// </summary>
    [TestFixture]
    public class GameLogicTest
    {
        private Mock<GameModel> mockedGameModel;

        /// <summary>
        /// If Model.Player.HitPoint reaches zero the GameState changes to Finnished.
        /// </summary>
        [Test]
        public void IfGameFinnished_ReturnsTrue()
        {
            // Mocking and Setup
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.Player.HitPoint = 0;
            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            // Act
            bool gameFinnished = logic.CheckGameEnd();

            // Assert
            Assert.That(gameFinnished, Is.EqualTo(true));
        }

        /// <summary>
        /// Tests if player's HitPoint is bigger than 0 than the gameState is not finnished.
        /// </summary>
        [Test]
        public void IfGameIsNotFinnished_ReturnsFalse()
        {
            // Mocking and Setup
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.Player.HitPoint = 2;
            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            // Act
            bool gameFinnshed = logic.CheckGameEnd();

            // Assert
            Assert.That(gameFinnshed, Is.EqualTo(false));
        }

        /// <summary>
        /// Checks if the logic properly sets the new state of the Game.
        /// </summary>
        /// <param name="newState">New GameState to be set</param>
        [TestCase(GameState.Paused)]
        public void WhenChangingGameState_StateIsChanged(GameState newState)
        {
            // Mocking and Setup
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.GameState = GameState.Running;
            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            // Act
            logic.GameStateSwitch(newState);

            // Assert
            Assert.That(this.mockedGameModel.Object.GameState, Is.EqualTo(GameState.Paused));
        }

        /// <summary>
        /// Checks if the Logic shoot method creates a new Projectile with proper coordinates.
        /// </summary>
        [Test]
        public void WhenPlayerShoots_NewProjectileCreatedWithGoodProperties()
        {
            // Mocking and Setup
            Player player = new Player(10, 10);

            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.Projectiles = new List<Projectile>();
            this.mockedGameModel.Object.Player = player;

            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            int numberOfProjectiles = this.mockedGameModel.Object.Projectiles.Count();

            // Act
            logic.PlayerShoot();

            // Assert
            Assert.That(this.mockedGameModel.Object.Projectiles.Count(), Is.EqualTo(numberOfProjectiles + 1));

            // Gets the latest projectile
            Projectile lastProjectile = this.mockedGameModel.Object.Projectiles[this.mockedGameModel.Object.Projectiles.Count() - 1];

            // Gets the Width of the Ship so the X position of the new projectile can be calculated properly
            RectangleGeometry shipBody = (RectangleGeometry)this.mockedGameModel.Object.Player.Shape();
            double shipSize = shipBody.Rect.Width;

            Assert.That(lastProjectile.X, Is.EqualTo(this.mockedGameModel.Object.Player.X + (shipSize / 2)));
            Assert.That(lastProjectile.Y, Is.EqualTo(this.mockedGameModel.Object.Player.Y));
        }

        /// <summary>
        /// Test to check if the player moves to the right direction.
        /// </summary>
        /// <param name="isMovingRight">True= right, false = left</param>
        [TestCase(true)]
        public void IfPlayerMoves_MovesToRightDirection(bool isMovingRight)
        {
            // Mocking and Setup
            Player player = new Player(10, 10);
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.Player = player;
            double originalPosition = this.mockedGameModel.Object.Player.X; // this will be checked in the assert section

            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            // Act
            logic.PlayerMove(isMovingRight);

            // Assert
            Assert.That(this.mockedGameModel.Object.Player.X, Is.EqualTo(originalPosition + 3));
        }

        /// <summary>
        /// Checks if the colsion checks returns true in case of collision between Player and Projectile
        /// </summary>
        [TestCase]
        public void IfPlayerCollidesWithProjectile_ReturnsTrue()
        {
            // Mock
            Player player = new Player(10, 10);
            Projectile collidesWith = new Projectile(10, 10, true, new Ufo());
            Projectile doesNotCollidesWith = new Projectile(1, 1, true, new Ufo());
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.Player = player;

            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            // Act
            bool collided = logic.CollisionCheck(collidesWith, this.mockedGameModel.Object.Player);
            bool notCollided = logic.CollisionCheck(doesNotCollidesWith, this.mockedGameModel.Object.Player);

            // Assert
            Assert.That(collided, Is.EqualTo(true));
            Assert.That(notCollided, Is.EqualTo(false));
        }

        /// <summary>
        /// Checks if the upon the death of a given gameItem it is removed from the GameModel
        /// </summary>
        [TestCase]
        public void WhenGameItemDies_ItIsRemovedFromTheModel()
        {
            // Mock
            Projectile projectile = new Projectile(10, 10, true, new Ufo());
            projectile.HitPoint = 0;

            Ufo ufo = new Ufo();
            ufo.HitPoint = 0;

            Shield shield = new Shield();
            shield.HitPoint = 0;

            List<Projectile> projectiles = new List<Projectile>();
            List<Ufo> ufos = new List<Ufo>();
            List<Shield> shields = new List<Shield>();

            this.mockedGameModel = new Mock<GameModel>();

            this.mockedGameModel.Object.Projectiles = projectiles;
            this.mockedGameModel.Object.Ufos = ufos;
            this.mockedGameModel.Object.Shields = shields;

            this.mockedGameModel.Object.Projectiles.Add(projectile);
            this.mockedGameModel.Object.Ufos.Add(ufo);
            this.mockedGameModel.Object.Shields.Add(shield);

            GameLogic logic = new GameLogic();
            logic.Model = this.mockedGameModel.Object;

            // Act
            Assert.That(logic.Model.Projectiles.Count(), Is.EqualTo(1));
            logic.DeathCheck(projectile, typeof(Projectile));
            Assert.That(logic.Model.Projectiles.Count(), Is.EqualTo(0));
        }
    }
}
