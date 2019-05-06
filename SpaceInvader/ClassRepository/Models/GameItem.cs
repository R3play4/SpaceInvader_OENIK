// <copyright file="GameItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    public abstract class GameItem
    {
        public GameItem()
        {
        }

        public GameItem(double y, double x)
        {
            this.Y = y;
            this.X = x;
        }

        public double Y { get; set; }

        public double X { get; set; }

        public double R { get; set; }

        public int HitPoint { get; set; }

        // public string ImgPath { get; set; }
        public void TakeDamage() => this.HitPoint--;
    }
}
