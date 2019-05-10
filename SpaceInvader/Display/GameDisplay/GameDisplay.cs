using ClassRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using GlobalSettings;
using System.Windows.Media.Imaging;

namespace Display.GameDisplay
{
    public class GameDisplay
    {
        GameModel gameModel;

        public GameDisplay(GameModel gameModel)
        {
            this.gameModel = gameModel;
        }

        private void DrawSpacship(DrawingContext context)
        {
            double halfWidth = Settings.ShipSize / 2;
            double halfHeight = Settings.ShipSize / 2;

            ImageBrush spaceshipBg = new ImageBrush(new BitmapImage(new Uri(Settings.SpaceShipBackground, UriKind.Relative)));
            RectangleGeometry playerRectGeometry = (RectangleGeometry)gameModel.Player.Shape();
            Rect half = playerRectGeometry.Rect;
            context.DrawRectangle(spaceshipBg, null, half);
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
            //dg.Children.Add(new GeometryDrawing(Settings.ShipColor, new Pen(Settings.ShipFrameColor, 1),
            //    this.gameModel.Player.Shape()));

            // Ufo-s
            foreach (UFO ufo in gameModel.UFOs)
            {
                 dg.Children.Add(new GeometryDrawing(GetUfoColor(ufo.Points), new Pen(GetUfoColor(ufo.Points), 1), ufo.Shape()));
            }

            // Shields
            foreach (Shield shield in gameModel.Shields)
            {
                dg.Children.Add(new GeometryDrawing(Settings.ShieldColor, new Pen(Settings.ShieldColor, 1), shield.Shape()));
            }

            // Projectiles
            foreach (Projectile projectile in gameModel.Projectiles)
            {
                dg.Children.Add(new GeometryDrawing(Settings.ProjectileColor, new Pen(Settings.ProjectileColor, 1), projectile.Shape()));
            }

            // Scores
            FormattedText text = new FormattedText(gameModel.Score.ToString(),
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Comic-Sans"),
                15,
                Brushes.White);

            dg.Children.Add(new GeometryDrawing(null, new Pen(Brushes.White, 1), text.BuildGeometry(new Point(10, 10))));


            context.DrawDrawing(dg);
            DrawSpacship(context);
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
