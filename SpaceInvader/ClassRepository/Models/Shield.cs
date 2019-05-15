// <copyright file="Shield.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    /// <summary>
    /// Shield object
    /// </summary>
    public class Shield : GameItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shield"/> class.
        /// </summary>
        public Shield()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shield"/> class.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Shield(double x, double y)
            : base(x, y)
        {
            this.HitPoint = 4;
        }

        /// <summary>
        /// Gets the shape data of the shield.
        /// </summary>
        /// <returns>geometry data of the shield</returns>
        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.ShieldWidth, Settings.ShieldHeight));
        }
    }
}
