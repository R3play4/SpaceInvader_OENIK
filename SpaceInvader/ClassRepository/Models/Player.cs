// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    /// <summary>
    /// Player game item
    /// </summary>
    public class Player : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Player(double x, double y)
            : base(x, y)
        {
            this.HitPoint = 3;
        }

        /// <summary>
        /// Changes the position with the difference.
        /// </summary>
        /// <param name="diff">the size of the difference that will change the postion of the player</param>
        public void Move(double diff)
        {
            this.X += diff;
        }

        /// <summary>
        /// returns data about the shape of the player.
        /// </summary>
        /// <returns>the shape of the Palyer</returns>
        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.ShipSize, Settings.ShipSize));
        }

        /// <summary>
        /// creates a new projectile at the midle of the players body.
        /// </summary>
        /// <returns>a new projectile</returns>
        public Projectile Shoot()
        {
            return new Projectile(this.X + (Settings.ShipSize / 2), this.Y, true, this);
        }
    }
}
