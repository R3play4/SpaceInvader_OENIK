using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    public class Player : GameItem
    {
        public Player(int y, int x) : base(y,x)
        {
            this.r = 10;
            this.HitPoint = 3;
        }
        public void Move(bool isMovingRight)
        {
            if (isMovingRight)
            {
                this.x++;
            }
            else
            {
                this.x--;
            }
        }

        public Projectile Shoot()
        {
            return new Projectile(this.y + this.r, this.x, true, this);
        }
    }
}
