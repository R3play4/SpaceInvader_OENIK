// <copyright file="Projectile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    public class Projectile : GameItem
    {
        public Projectile()
        {
        }

        public Projectile(int y, int x, bool direction, GameItem sourceObject)
            : base(y, x)
        {
            this.SourceObject = sourceObject;
            this.IsMovingUp = direction;
            this.R = 2;
        }

        public bool IsMovingUp { get; set; }

        public GameItem SourceObject { get; set; }

        public void Move()
        {
            if (this.IsMovingUp)
            {
                this.Y++;
            }
            else
            {
                this.Y--;
            }
        }
    }
}
