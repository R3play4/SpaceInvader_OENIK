// <copyright file="Shield.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository
{
    public class Shield : GameItem
    {
        public Shield()
        {
        }

        public Shield(double x, double y)
            : base(x, y)
        {
            this.HitPoint = 4;
            this.R = 20;
        }
    }
}
