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

        public Player(double x, double y)
            : base(x, y)
        {
            this.R = 10;
            this.HitPoint = 3;
        }

        public void Move(double diff)
        {
            this.X += diff;
        }

        public Projectile Shoot()
        {
            return new Projectile(this.X, this.Y-this.R, true, this);
        }
    }
}
