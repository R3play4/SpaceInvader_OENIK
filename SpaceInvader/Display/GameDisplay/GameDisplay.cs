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
            /*
            double halfWidth = Settings.ShipSize / 2;
            double halfHeight = Settings.ShipSize / 2;
            */
            ImageBrush spaceshipBg = new ImageBrush(new BitmapImage(new Uri(Settings.SpaceShipBackground, UriKind.Relative)));
            RectangleGeometry playerRectGeometry = (RectangleGeometry)gameModel.Player.Shape();
            //Rect half = playerRectGeometry.Rect;
            //half.X = gameModel.Player.X - (Settings.ShipSize/2);
            //half.Y = gameModel.Player.Y - (Settings.ShipSize / 2);
            //half.Width = 50;
            //half.Height = 50;
            context.DrawRectangle(spaceshipBg, null, playerRectGeometry.Rect);
        }

        private void DrawPlayerLife(DrawingContext context, double newX , double newY)
        {
            ImageBrush playerLife = new ImageBrush(new BitmapImage(new Uri(Settings.PlayerLife, UriKind.Relative)));
            RectangleGeometry playerRectGeometry = (RectangleGeometry)gameModel.Player.Shape();
            Rect rect = playerRectGeometry.Rect;
            rect.X = newX;
            rect.Y = newY;
            rect.Width = 15;
            rect.Height = 15;
            context.DrawRectangle(playerLife, null, rect);
        }

        private void DrawUFO(DrawingContext context)
        {
            //ImageBrush ufo_1 = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_1, UriKind.Relative)));
            //ImageBrush ufo_2 = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_2, UriKind.Relative)));
            //ImageBrush ufo_3 = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_3, UriKind.Relative)));

            foreach (var ufo in gameModel.UFOs)
            {
                RectangleGeometry actualGeometry = (RectangleGeometry)ufo.Shape();
                context.DrawRectangle(GetUFOImg(ufo.Points), null, actualGeometry.Rect);
            }
        }

        private void DrawShield(DrawingContext context)
        {
            foreach (var shield in gameModel.Shields)
            {
                RectangleGeometry actualGeometry = (RectangleGeometry)shield.Shape();
                context.DrawRectangle(GetShieldImage(shield.HitPoint), null, actualGeometry.Rect);
            }
        }

        public void Display(DrawingContext context)
        {
            DrawingGroup dg = new DrawingGroup();
            //context.DrawRectangle(Config.BackgroundColor, new Pen(Config.FrameColor, Config.FrameSize), new Rect(0, 0, Config.WindowWidth - Config.FrameSize * 4, Config.WindowHeight - Config.FrameSize * 4));
            //new Pen(Config.FrameColor, Config.FrameSize)

            // Background
            dg.Children.Add(new GeometryDrawing(Settings.BackgroundColor, new Pen(Settings.FrameColor, Settings.FrameSize),
                new RectangleGeometry(new Rect(0, 0, Settings.WindowWidth - Settings.FrameSize * 4, Settings.WindowHeight - Settings.FrameSize * 2))
                ));

            // Playership
            //dg.Children.Add(new GeometryDrawing(Settings.ShipColor, new Pen(Settings.ShipFrameColor, 1),
            //    this.gameModel.Player.Shape()));

            // Ufo-s
            //foreach (UFO ufo in gameModel.UFOs)
            //{
            //     dg.Children.Add(new GeometryDrawing(GetUfoColor(ufo.Points), new Pen(GetUfoColor(ufo.Points), 1), ufo.Shape()));
            //}

            // Shields
            //foreach (Shield shield in gameModel.Shields)
            //{
            //    dg.Children.Add(new GeometryDrawing(Settings.ShieldColor, new Pen(Settings.ShieldColor, 1), shield.Shape()));
            //}

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

            dg.Children.Add(new GeometryDrawing(null, new Pen(Brushes.White, 1), text.BuildGeometry(new Point(10, 5))));

            


            context.DrawDrawing(dg);
            DrawSpacship(context);
            DrawUFO(context);
            DrawShield(context);

            // Player lifes
            // position of the first Player life sprite.

            for (int i = 0; i < gameModel.Player.HitPoint; i++)
            {
                if(i == 0)
                    DrawPlayerLife(context, Settings.WindowWidth - 70, 5);
                if(i == 1)
                    DrawPlayerLife(context, Settings.WindowWidth - 50, 5);
                if(i== 2)
                {
                    DrawPlayerLife(context, Settings.WindowWidth - 30, 5);
                }
            }

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

        public ImageBrush GetUFOImg(int point)
        {
            switch (point)
            {
                case 10:
                    return Settings.UFO_1_Image;
                case 20:
                    return Settings.UFO_2_Image;
                case 40:
                    return Settings.UFO_3_Image;
                case 100:
                    return Settings.UFO_Side_Image;
                default:
                    return null;
            }
        }

        public ImageBrush GetShieldImage(int hitPoints)
        {
            switch (hitPoints)
            {
                case 3:
                    return Settings.Shield_1_Image;
                case 2:
                    return Settings.Shield_2_Image;
                case 1:
                    return Settings.Shield_3_Image;
                case 0:
                    return Settings.Shield_4_Image;
                default:
                    return Settings.Shield_1_Image;
            }
        }
    }
}
