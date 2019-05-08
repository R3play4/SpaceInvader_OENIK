// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

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

        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.ShipSize, Settings.ShipSize));
        }

        public Projectile Shoot()
        {
            return new Projectile(this.X, this.Y-this.R, true, this);
        }
    }
}
