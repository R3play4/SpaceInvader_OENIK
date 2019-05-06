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

        public Shield(double y = 20, double x = 30)
            : base(y, x)
        {
            this.HitPoint = 4;
            this.R = 20;
        }
    }
}
