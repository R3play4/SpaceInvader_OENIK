using ClassRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GlobalSettings;

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
            dg.Children.Add(new GeometryDrawing(Settings.BackgroundColor, new Pen(Settings.FrameColor, Settings.FrameSize),
                new RectangleGeometry(new Rect(0, 0, Settings.WindowWidth - Settings.FrameSize * 4, Settings.WindowHeight - 32 - Settings.FrameSize * 2))
                ));

            // Playership
            dg.Children.Add(new GeometryDrawing(Settings.ShipColor, new Pen(Settings.ShipFrameColor, 1),
                // ide kell majd GameItem.Rect
                new RectangleGeometry(new Rect(gameModel.Player.X, gameModel.Player.Y, 15, 15))
                ));

            // Ufo-s
            foreach (UFO ufo in gameModel.UFOs)
            {
                 dg.Children.Add(new GeometryDrawing(GetUfoColor(ufo.Points), new Pen(GetUfoColor(ufo.Points), 1),
                    new RectangleGeometry(new Rect(ufo.X, ufo.Y, 15, 15))
                    ));
            }

            // Shields
            foreach (Shield shield in gameModel.Shields)
            {
                dg.Children.Add(new GeometryDrawing(Settings.ShieldColor, new Pen(Settings.ShieldColor, 1),
                    new RectangleGeometry(new Rect(shield.X, shield.Y, 40, 20))
                    ));
            }

            // Projectiles
            foreach (Projectile projectile in gameModel.Projectiles)
            {
                dg.Children.Add(new GeometryDrawing(Settings.ProjectileColor, new Pen(Settings.ProjectileColor, 1),
                    new RectangleGeometry(new Rect(projectile.X, projectile.Y, 2, 6))
                    ));
            }

            context.DrawDrawing(dg);
        }

        public Brush GetUfoColor(int point)
        {
            switch (point)
            {
                case 10:
                    return Settings.UFO1Color;
                case 20:
                    return Settings.UFO2Color;
                case 40:
                    return Settings.UFO3Color;
                default:
                    return null;
            }
        }
    }
}
