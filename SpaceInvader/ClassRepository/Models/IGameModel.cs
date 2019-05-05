// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClassRepository.Model
{
    using System.Collections.Generic;

    public interface IGameModel
    {
        List<UFO> UFOs { get; set; }

        List<Projectile> Projectiles { get; set; }

        List<Shield> Shields { get; set; }

        Player Player { get; set; }

        GameState GameState { get; set; }

        int Score { get; set; }
    }
}
