// <copyright file="GameLogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GameLogic.Test
{
    using ClassRepository;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestFixture]
    public class GameLogicTest
    {
        private Mock<GameModel> mockedGameModel;

        //[SetUp]
        //public void SetUp()
        //{
        //    // initiates mocked GameModel
        //    this.mockedGameModel = new Mock<GameModel>();

        //    // sets game state to running.
        //    this.mockedGameModel.Object.GameState = GameState.Running;
        //}

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

    }
}
