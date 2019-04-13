using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository.XmlHandler
{
    public interface IGameControl
    {
        GameModel Load(string filePath);
        void Save(string filePath, GameModel model);
    }
}
