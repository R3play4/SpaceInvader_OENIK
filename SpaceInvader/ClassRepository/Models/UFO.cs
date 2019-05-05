// <copyright file="UFO.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    public class UFO : GameItem
    {
        public UFO()
        {
        }

        public UFO(int x, int y, int points)
            : base(x, y)
        {
            this.HitPoint = 1;
            this.Points = points;
        }

        public int Points { get; set; }

        public void Move()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i % 2 == 0)
                    {
                        this.X++;
                    }
                    else
                    {
                        this.X--;
                    }
                }

                this.Y--;
            }
        }

        public Projectile Shoot()
        {
            return new Projectile(this.Y + this.R, this.X, false, this);
        }
    }
}
