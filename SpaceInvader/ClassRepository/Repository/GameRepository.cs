// <copyright file="GameRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository.Repository
{
    using System.IO;
    using System.Xml.Serialization;
    using ClassRepository.Model;

    /// <summary>
    /// Implementing class of IGameRepository (XML handling)
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(GameModel));

        /// <summary>
        /// Loads the state of the Game from an XML file.
        /// </summary>
        /// <param name="filePath">path to the xml that represents the game state</param>
        /// <returns>returns the GameModel that represents the laoded state of the game</returns>
        public GameModel LoadGameState(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            GameModel model = new GameModel();
            model = (GameModel)this.serializer.Deserialize(reader);
            return model;
        }

        /// <summary>
        /// Saves the current state of the game into an xml file.
        /// </summary>
        /// <param name="filePath">Path where the file is saved to</param>
        /// <param name="currentState">The state that will be saved,</param>
        public void SaveGameState(string filePath, GameModel currentState)
         {
            StreamWriter writer = new StreamWriter(filePath);
            this.serializer.Serialize(writer, currentState);
            writer.Close();
        }
    }
}
