// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows.Media;

    /// <summary>
    /// This class is used in the GameModel to define a state of the game.
    /// </summary>
    public abstract class GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameItem"/> class.
        /// </summary>
        protected GameItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameItem"/> class.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        protected GameItem(double x, double y)
        {
            this.Y = y;
            this.X = x;
        }

        /// <summary>
        /// Gets or sets Y coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets X coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the number of times an Item was hit by a projectile.
        /// </summary>
        public int HitPoint { get; set; }

        /// <summary>
        /// Reduces the HitPoint property.
        /// </summary>
        public void TakeDamage() => this.HitPoint--;

        /// <summary>
        /// Returns the shape of the item.
        /// </summary>
        /// <returns>Geometry of the item</returns>
        public abstract Geometry Shape();
    }
}
