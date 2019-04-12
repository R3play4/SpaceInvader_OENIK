using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    public class Shield : GameItem
    {
        public Shield(int y, int x) : base(y, x)
        {
            this.HitPoint = 4;
            this.r = 20;
        }
    }
}
