using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GlobalSettings
{
    public class Settings
    {
        // Window Config
        public static double WindowWidth = 400;
        public static double WindowHeight = 532;
        public static int FrameSize = 0;

        public static Brush BackgroundColor = Brushes.Black;
        public static Brush FrameColor = Brushes.Red;

        // Player ship
        public static Brush ShipColor = Brushes.White;
        public static Brush ShipFrameColor = Brushes.Green;
        public const int ShipSize = 40; // 15
        public const int PlayerStepSize = 3;

        // UFO
        public static Brush UFO1Color = Brushes.Orange;
        public static Brush UFO2Color = Brushes.Yellow;
        public static Brush UFO3Color = Brushes.LightBlue;
        public const int UfoShipSize = 15; // 15
        public const double UfoSideStepSize = 5;

        // Shield
        public static Brush ShieldColor = Brushes.Green;
        public const int ShieldWidth = 40;
        public const int ShieldHeight = 15;

        // Projectile
        public static Brush ProjectileColor = Brushes.White;
        public const int ProjectileLength = 6;
        public const int ProjectileWidth = 2;

        // Sprites
        public static string SpaceShipBackground = "..\\..\\Images\\SpaceShip.png";
        public static string PlayerLife = "..\\..\\Images\\Heart.png";
        public static string UFO_1 = "..\\..\\Images\\UFO1.png";
        public static string UFO_2 = "..\\..\\Images\\UFO2.png";
        public static string UFO_3 = "..\\..\\Images\\UFO3.png";
        public static string UFO_Side = "..\\..\\Images\\UFO_Side.png";
        public static string Shield_1 = "..\\..\\Images\\Shield_1.png";
        public static string Shield_2 = "..\\..\\Images\\Shield_2.png";
        public static string Shield_3 = "..\\..\\Images\\Shield_3.png";
        public static string Shield_4 = "..\\..\\Images\\Shield_4.png";
        public static string MainMenuBackground = "..\\..\\Images\\MainMenuBG.png";

        // Brushes
        public static ImageBrush UFO_1_Image = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_1, UriKind.Relative)));
        public static ImageBrush UFO_2_Image = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_2, UriKind.Relative)));
        public static ImageBrush UFO_3_Image = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_3, UriKind.Relative)));
        public static ImageBrush UFO_Side_Image = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_Side, UriKind.Relative)));

        public static ImageBrush Shield_1_Image = new ImageBrush(new BitmapImage(new Uri(Settings.Shield_1, UriKind.Relative)));
        public static ImageBrush Shield_2_Image = new ImageBrush(new BitmapImage(new Uri(Settings.Shield_2, UriKind.Relative)));
        public static ImageBrush Shield_3_Image = new ImageBrush(new BitmapImage(new Uri(Settings.Shield_3, UriKind.Relative)));
        public static ImageBrush Shield_4_Image = new ImageBrush(new BitmapImage(new Uri(Settings.Shield_4, UriKind.Relative)));
    }
}
