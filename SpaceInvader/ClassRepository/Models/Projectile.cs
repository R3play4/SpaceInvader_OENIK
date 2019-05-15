// <copyright file="Projectile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    /// <summary>
    /// Projectile GameItem
    /// </summary>
    public class Projectile : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        public Projectile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Projectile"/> class.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="direction"> true = right, false = left</param>
        /// <param name="sourceObject">object that shot the projectile</param>
        public Projectile(double x, double y, bool direction, GameItem sourceObject)
            : base(x, y)
        {
            this.HitPoint = 1;
            this.SourceObject = sourceObject;
            this.IsMovingUp = direction;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the projectile is moving up or down
        /// </summary>
        public bool IsMovingUp { get; set; }

        /// <summary>
        /// Gets or sets the object that shot the projectile
        /// </summary>
        public GameItem SourceObject { get; set; }

        /// <summary>
        /// Gets the shape of the object.
        /// </summary>
        /// <returns>Geometry data of the shape of the object</returns>
        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.ProjectileWidth, Settings.ProjectileLength));
        }

        /// <summary>
        /// Changes the position of the projectile. Either up or down by 5 units
        /// </summary>
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
