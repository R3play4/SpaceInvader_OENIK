// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    using System.Windows.Media;

    public abstract class GameItem
    {
        public GameItem()
        {
        }

        public GameItem(double x, double y)
        {
            this.Y = y;
            this.X = x;
        }

        public double Y { get; set; }

        public double X { get; set; }

        public int HitPoint { get; set; }

        // public string ImgPath { get; set; }
        public void TakeDamage() => this.HitPoint--;

        public abstract Geometry Shape();
    }
}
