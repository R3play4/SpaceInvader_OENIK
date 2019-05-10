using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
        public const int ShipSize = 15;

        // UFO
        public static Brush UFO1Color = Brushes.Orange;
        public static Brush UFO2Color = Brushes.Yellow;
        public static Brush UFO3Color = Brushes.LightBlue;

        // Shield
        public static Brush ShieldColor = Brushes.Green;
        public const int ShieldWidth = 40;
        public const int ShieldHeight = 15;

        // Projectile
        public static Brush ProjectileColor = Brushes.Orange;
        public const int ProjectileLength = 6;
        public const int ProjectileWidth = 2;

        // Sprites
        public static string SpaceShipBackground = "SpaceShip.jpg";
    }
}
