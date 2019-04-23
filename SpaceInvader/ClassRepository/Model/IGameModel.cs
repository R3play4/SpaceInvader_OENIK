using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository.Model
{
    public interface IGameModel
    {
        List<UFO> UFOs { get; set; }

        List<Projectile> Projectiles { get; set; }

        List<Shield> Shields { get; set; }

        Player Player { get; set; }

        GameState GameState { get; set; }

        int Score { get; set; }
    }
}
