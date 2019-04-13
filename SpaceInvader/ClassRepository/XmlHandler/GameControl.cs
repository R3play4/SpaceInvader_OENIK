using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassRepository.XmlHandler
{
    public class GameControl : IGameControl
    {
        XmlSerializer serializer;

        public GameControl()
        {
            this.serializer = new XmlSerializer(typeof(GameModel));
        }

        public GameModel Load(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            GameModel model;
            model = (GameModel)serializer.Deserialize(reader);
            reader.Close();
            return model;
        }

        public void Save(string filePath, GameModel model)
        {
            StreamWriter writer = new StreamWriter(filePath);
            serializer.Serialize(writer, model);
            writer.Close();
        }
    }
}
