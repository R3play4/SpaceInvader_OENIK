// <copyright file="GameLogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic.Test
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
        /// 
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
    }
}
