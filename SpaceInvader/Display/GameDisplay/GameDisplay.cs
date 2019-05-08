using ClassRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Display.GameDisplay
{
    public class GameDisplay
    {
        GameModel gameModel;

        public GameDisplay(GameModel gameModel)
        {
            this.gameModel = gameModel;
        }

        public void Display(DrawingContext context)
        {
            DrawingGroup dg = new DrawingGroup();
            //context.DrawRectangle(Config.BackgroundColor, new Pen(Config.FrameColor, Config.FrameSize), new Rect(0, 0, Config.WindowWidth - Config.FrameSize * 4, Config.WindowHeight - Config.FrameSize * 4));
            //new Pen(Config.FrameColor, Config.FrameSize)

            // Background
            dg.Children.Add(new GeometryDrawing(Config.BackgroundColor, new Pen(Config.FrameColor, Config.FrameSize),
                new RectangleGeometry(new Rect(0, 0, Config.WindowWidth - Config.FrameSize * 4, Config.WindowHeight - 32 - Config.FrameSize * 2))
                ));

            // Playership
            dg.Children.Add(new GeometryDrawing(Config.ShipColor, new Pen(Config.ShipFrameColor, 1),                
                this.gameModel.Player.Shape()));

            // Ufo-s
            foreach (UFO ufo in gameModel.UFOs)
            {
                 dg.Children.Add(new GeometryDrawing(GetUfoColor(ufo.Points), new Pen(GetUfoColor(ufo.Points), 1), ufo.Shape()));
            }

            // Shields
            foreach (Shield shield in gameModel.Shields)
            {
                dg.Children.Add(new GeometryDrawing(Config.ShieldColor, new Pen(Config.ShieldColor, 1), shield.Shape()));
            }

            // Projectiles
            foreach (Projectile projectile in gameModel.Projectiles)
            {
                dg.Children.Add(new GeometryDrawing(Config.ProjectileColor, new Pen(Config.ProjectileColor, 1), projectile.Shape()));
            }

            context.DrawDrawing(dg);
        }

        public Brush GetUfoColor(int point)
        {
            switch (point)
            {
                case 10:
                    return Config.UFO1Color;
                case 20:
                    return Config.UFO2Color;
                case 40:
                    return Config.UFO3Color;
                default:
                    return null;
            }
        }
    }
}
