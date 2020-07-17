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
using ClassRepository.Model;

namespace Display.GameDisplay
{
    public class SpaceInvaderDisplay
    {
        IGameModel gameModel;

        public SpaceInvaderDisplay(IGameModel gameModel)
        {
            this.gameModel = gameModel;
        }

        private void DrawSpacship(DrawingContext context)
        {
            ImageBrush spaceshipBg = new ImageBrush(new BitmapImage(new Uri(Settings.SpaceShipBackground, UriKind.Relative)));
            RectangleGeometry playerRectGeometry = (RectangleGeometry)gameModel.Player.Shape();
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

        private void DrawMainMenuWindow()
        {
            Window mainMenu = new Window();
            mainMenu.ShowDialog();

        }

        private void DrawUFO(DrawingContext context)
        {

            foreach (var ufo in gameModel.Ufos)
            {
                RectangleGeometry actualGeometry = (RectangleGeometry)ufo.Shape();
                context.DrawRectangle(GetUfoImg(ufo.Points), null, actualGeometry.Rect);
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

        public void DisplayGameOver(DrawingContext context)
        {
            Rect geometry = new Rect(0, 0, Settings.WindowWidth, Settings.WindowHeight);
            context.DrawRectangle(Settings.GameOverBrush, null, geometry);
        }

        public void Display(DrawingContext context)
        {
            DrawingGroup dg = new DrawingGroup();

            // Background
            dg.Children.Add(new GeometryDrawing(Settings.BackgroundColor, new Pen(Settings.FrameColor, Settings.FrameSize),
                new RectangleGeometry(new Rect(0, 0, Settings.WindowWidth - Settings.FrameSize * 4, Settings.WindowHeight - Settings.FrameSize * 2))
                ));

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
                Brushes.White,
                VisualTreeHelper.GetDpi(new GameControl()).PixelsPerDip);

            dg.Children.Add(new GeometryDrawing(null, new Pen(Brushes.White, 1), text.BuildGeometry(new Point(10, 5))));

            context.DrawDrawing(dg);
            DrawSpacship(context);
            DrawUFO(context);
            DrawShield(context);

            // Player lifes
            // position of the first Player life sprite.

            for (int i = 1; i <= gameModel.Player.HitPoint; i++)
            {
                DrawPlayerLife(context, Settings.WindowWidth - i*20, 5);
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

        public ImageBrush GetUfoImg(int point)
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
