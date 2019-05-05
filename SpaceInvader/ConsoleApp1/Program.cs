using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassRepository;
using ClassRepository.Repository;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<UFO> ufos = new List<UFO>();
            ufos.Add(new UFO(10, 10, 50));
            ufos.Add(new UFO(10, 20, 30));

            List<Projectile> projectiles = new List<Projectile>();

            List<Shield> shields = new List<Shield>();
            shields.Add(new Shield(50, 60));
            shields.Add(new Shield(70, 60));
            shields.Add(new Shield(90, 60));

            Player player = new Player(70, 80);

            GameRepository repo = new GameRepository();
            GameModel model = new GameModel(ufos, projectiles, shields, player);
            repo.SaveGameState("testSave.xml", model);
            Console.WriteLine("Save OK");
            Console.Read();
        }
    }
}
