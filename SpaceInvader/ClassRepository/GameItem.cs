using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassRepository
{
    public abstract class GameItem
    {
        public int y { get; set; }
        public int x { get; set; }
        public int r { get; set; }
        public int HitPoint { get; set; }
        //public string ImgPath { get; set; }

        public GameItem(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public abstract void Move();
        public void TakeDamage() => this.HitPoint--;
    }
}
