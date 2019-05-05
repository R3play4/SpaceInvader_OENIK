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

        public GameItem(int y, int x)
        {
            this.Y = y;
            this.X = x;
        }

        public int Y { get; set; }

        public int X { get; set; }

        public int R { get; set; }

        public int HitPoint { get; set; }

        // public string ImgPath { get; set; }
        public void TakeDamage() => this.HitPoint--;
    }
}
