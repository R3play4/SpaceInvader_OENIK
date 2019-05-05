// <copyright file="GameRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository.Repository
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Implementing class of IGameRepository (XML handling)
    /// </summary>
    public class GameRepository : IGameRepository
    {
        private XmlSerializer serializer = new XmlSerializer(typeof(GameModel));

        public GameModel LoadGameState(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            GameModel model = new GameModel();
            model = (GameModel)this.serializer.Deserialize(reader);
            return model;
        }

        public void SaveGameState(string filePath, GameModel currentState)
        {
            StreamWriter writer = new StreamWriter(filePath);
            this.serializer.Serialize(writer, currentState);
            writer.Close();
        }
    }
}
