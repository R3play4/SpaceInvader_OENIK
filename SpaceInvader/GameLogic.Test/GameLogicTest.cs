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
    using ClassRepository;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class GameLogicTest
    {
        private Mock<GameModel> mockedGameModel;

        [Test]
        public void IfGameFinnished_ReturnsTrue()
        {
            // Mocking and Setup
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.GameState = GameState.Finished;
            GameLogic logic = new GameLogic(this.mockedGameModel.Object);

            // Act
            bool gameFinnished = logic.GameEnd();

            // Assert
            Assert.That(gameFinnished, Is.EqualTo(true));
        }

        [Test]
        public void IfGameIsNotFinnished_ReturnsFalse()
        {
            // Mocking and Setup
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.GameState = GameState.Running;
            GameLogic logic = new GameLogic(this.mockedGameModel.Object);

            // Act
            bool gameFinnshed = logic.GameEnd();

            // Assert
            Assert.That(gameFinnshed, Is.EqualTo(false));
        }

        [TestCase(GameState.Paused)]
        public void WhenChangingGameState_StateIsChanged(GameState newState)
        {
            // Mocking and Setup
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.GameState = GameState.Running;
            GameLogic logic = new GameLogic(this.mockedGameModel.Object);

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

            GameLogic logic = new GameLogic(this.mockedGameModel.Object);

            int numberOfProjectiles = this.mockedGameModel.Object.Projectiles.Count();

            // Act
            logic.PlayerShoot();

            // Assert
            Assert.That(this.mockedGameModel.Object.Projectiles.Count(), Is.EqualTo(numberOfProjectiles + 1));

            // Gets the latest projectile
            Projectile lastProjectile = this.mockedGameModel.Object.Projectiles[this.mockedGameModel.Object.Projectiles.Count() - 1];

            Assert.That(lastProjectile.x, Is.EqualTo(this.mockedGameModel.Object.Player.x));
            Assert.That(lastProjectile.y, Is.EqualTo(this.mockedGameModel.Object.Player.y));
        }

        [TestCase(true)]
        public void IfPlayerMoves_MovesToRightDirection(bool isMovingRight)
        {
            // Mocking and Setup
            Player player = new Player(10, 10);
            this.mockedGameModel = new Mock<GameModel>();
            this.mockedGameModel.Object.Player = player;

            GameLogic logic = new GameLogic(this.mockedGameModel.Object);

            // Act
            logic.PlayerMove(isMovingRight);

            // Assert
            Assert.That(this.mockedGameModel.Object.Player.x, Is.EqualTo(11));
            Assert.That(this.mockedGameModel.Object.Player.y, Is.EqualTo(10));
        }
    }
}
