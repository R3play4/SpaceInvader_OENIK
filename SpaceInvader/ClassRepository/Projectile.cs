using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    class Projectile : GameItem
    {
        public Projectile(int y, int x) : base(y, x)
        {
            this.x = x;
            this.y = y;
            this.r = 2;
        }
        public override void Move()
        {
            throw new NotImplementedException();
        }
    }
}
