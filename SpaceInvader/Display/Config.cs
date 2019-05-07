using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Display
{
    public class Config
    {
        // Window Config
        public static double WindowWidth = 400;
        public static double WindowHeight = 532;
        public static int FrameSize = 4;

        public static Brush BackgroundColor = Brushes.Black;
        public static Brush FrameColor = Brushes.Red;

        //Player ship
        public static Brush ShipColor = Brushes.White;
        public static Brush ShipFrameColor = Brushes.Green;

        //UFO
        public static Brush UFO1Color = Brushes.Orange;
        public static Brush UFO2Color = Brushes.Yellow;
        public static Brush UFO3Color = Brushes.Blue;

        //Shield
        public static Brush ShieldColor = Brushes.Green;
    }
}
