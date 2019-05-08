// <copyright file="Projectile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    public class Projectile : GameItem
    {
        public Projectile()
        {
        }

        public Projectile(double x, double y, bool direction, GameItem sourceObject)
            : base(x, y)
        {
            this.HitPoint = 1;
            this.SourceObject = sourceObject;
            this.IsMovingUp = direction;
            this.R = 2;
        }

        public bool IsMovingUp { get; set; }

        public GameItem SourceObject { get; set; }

        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.ProjectileWidth, Settings.ProjectileLength));
        }

        public void Move()
        {
            if (this.IsMovingUp)
            {
                this.Y -= 5;
            }
            else
            {
                this.Y += 5;
            }
        }
    }
}
