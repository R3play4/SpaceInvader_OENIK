// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    public class Player : GameItem
    {
        public Player()
        {
        }

        public Player(double y, double x)
            : base(y, x)
        {
            this.R = 10;
            this.HitPoint = 3;
        }

        public void Move(double diff)
        {
            this.X += diff;
        }

        /*
        public void Move(bool isMovingRight)
        {
            if (isMovingRight)
            {
                this.X++;
            }
            else
            {
                this.X--;
            }
        }
        */

        public Projectile Shoot()
        {
            return new Projectile(this.Y + this.R, this.X, true, this);
        }
    }
}
