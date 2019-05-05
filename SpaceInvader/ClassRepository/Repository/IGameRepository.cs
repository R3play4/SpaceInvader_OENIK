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
        GameModel LoadGameState(string filePath);

        void SaveGameState(string filePath, GameModel currentState);
    }
}
