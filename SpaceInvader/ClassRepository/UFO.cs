using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    public class UFO : GameItem
    {
        public int Points { get; set; }
        public UFO(int x, int y, int points) : base(x,y)
        {
            this.Points = points;
        }
        public override void Move()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i % 2 == 0)
                    {
                        this.x++;
                    }
                    else
                    {
                        this.x--;
                    }                    
                }
                this.y--;
            }
        }

        public Projectile Shoot()
        {
            return new Projectile(this.y + this.r, this.x, false, this);
        }
    }
}
