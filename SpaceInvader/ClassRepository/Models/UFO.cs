// <copyright file="UFO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    public class UFO : GameItem
    {
        private int sidewaysMoveCount = 15;

        public UFO()
        {
        }

        public UFO(int x, int y, int points)
            : base(x, y)
        {
            this.HitPoint = 1;
            this.Points = points;
        }

        public int Points { get; set; }

        public void Move()
        {
            if (this.sidewaysMoveCount > 0)
            {
                this.X += 5;
                this.sidewaysMoveCount--;
            }
            else if (this.sidewaysMoveCount == 0)
            {
                this.MoveDown();
                this.sidewaysMoveCount--;
            }
            else if (this.sidewaysMoveCount >= -15)
            {
                this.X -= 5;
                this.sidewaysMoveCount--;
            }
            else
            {
                this.MoveDown();
                this.sidewaysMoveCount = 15;
            }
        }

        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.UfoShipSize, Settings.UfoShipSize));
        }

        public Projectile Shoot()
        {
            return new Projectile(this.X + (Settings.UfoShipSize/2), this.Y + this.R, false, this);
        }

        private void MoveDown() => this.Y += 10;
    }
}
