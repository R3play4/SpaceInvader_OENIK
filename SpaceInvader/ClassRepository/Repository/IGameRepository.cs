// <copyright file="IGameRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository.Repository
{
    using ClassRepository.Model;

    /// <summary>
    /// Interface for handling XML files for saving/loading
    /// </summary>
    public interface IGameRepository
    {
        /// <summary>
        /// Loads the state of the Game from an XML file.
        /// </summary>
        /// <param name="filePath">path to the xml that represents the game state</param>
        /// <returns>returns the GameModel that represents the laoded state of the game</returns>
        GameModel LoadGameState(string filePath);

        /// <summary>
        /// Saves the current state of the game into an xml file.
        /// </summary>
        /// <param name="filePath">Path where the file is saved to</param>
        /// <param name="currentState">The state that will be saved,</param>
        void SaveGameState(string filePath, GameModel currentState);
    }
}
