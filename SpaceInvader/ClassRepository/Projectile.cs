using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    public class Projectile : GameItem
    {
        public bool IsMovingUp { get; set; }
        public Projectile(int y, int x, bool direction) : base(y, x)
        {
            this.IsMovingUp = direction;
            this.r = 2;
        }
        public override void Move()
        {
            if (this.IsMovingUp)
            {
                this.y++;
            }
            else
            {
                this.y--;
            }
        }
    }
}
