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

        public Shield(int y = 20, int x = 30)
            : base(y, x)
        {
            this.HitPoint = 4;
            this.R = 20;
        }
    }
}
