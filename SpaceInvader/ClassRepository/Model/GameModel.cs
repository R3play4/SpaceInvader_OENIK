using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    public class GameModel
    {
        public List<UFO> UFOs { get; set; }
        public List<Projectile> Projectiles { get; set; }
        public List<Shield> Shields { get; set; }
        public Player Player { get; set; }
        public GameState GameState { get; set; }

        public GameModel(List<UFO> ufos, List<Projectile> projectiles, List<Shield> shields, Player player, GameState gamestate)
        {
            this.UFOs = ufos;
            this.Projectiles = projectiles;
            this.Shields = shields;
            this.Player = player;
            this.GameState = GameState;
        }

    }

    public enum GameState
    {
        Paused,
        Running,
        Finished
    }
}
