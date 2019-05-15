// <copyright file="UFO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    /// <summary>
    /// UFO game item.
    /// </summary>
    public class Ufo : GameItem
    {
        private int sidewaysMoveCount = 15;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ufo"/> class.
        /// </summary>
        public Ufo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ufo"/> class.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="points">points that ufo worth</param>
        public Ufo(int x, int y, int points)
            : base(x, y)
        {
            this.HitPoint = 1;
            this.Points = points;
        }

        /// <summary>
        /// Gets or sets the points that a player can get for this UFO
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Moves the ufo side ways by 15. Than changes direction and lowers their position on the Y axis
        /// </summary>
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

        /// <summary>
        /// Moves the UFO side ways
        /// </summary>
        /// <param name="diff">the difference by with the UFO is moved</param>
        public void MoveSideWays(double diff)
        {
            this.X += diff;
        }

        /// <summary>
        /// Gets the shape of the UFO
        /// </summary>
        /// <returns>Shape data of the ufo</returns>
        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.UfoShipSize, Settings.UfoShipSize));
        }

        /// <summary>
        /// Create a projectile at the midle of the UFo's positon
        /// </summary>
        /// <returns>new projectile</returns>
        public Projectile Shoot()
        {
            return new Projectile(this.X + (Settings.UfoShipSize / 2), this.Y, false, this);
        }

        private void MoveDown() => this.Y += 15;
    }
}
