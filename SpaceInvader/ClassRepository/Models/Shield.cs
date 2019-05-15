// <copyright file="Shield.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows;
    using System.Windows.Media;
    using GlobalSettings;

    public class Shield : GameItem
    {
        public Shield()
        {
        }

        public Shield(double x, double y)
            : base(x, y)
        {
            this.HitPoint = 4;
        }

        public override Geometry Shape()
        {
            return new RectangleGeometry(new Rect(this.X, this.Y, Settings.ShieldWidth, Settings.ShieldHeight));
        }
    }
}
