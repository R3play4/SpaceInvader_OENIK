using ClassRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Display.Display
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

            // Background
            dg.Children.Add(new GeometryDrawing(Config.BackgroundColor, new Pen(Config.FrameColor, Config.FrameSize),
                new RectangleGeometry(new Rect(0, 0, Config.WindowWidth, Config.WindowHeight));
                ));

            // Playership
            dg.Children.Add(new GeometryDrawing(Config.ShipColor, new Pen(Config.ShipFrameColor, 1),
                // ide kell majd GameItem.Rect
                new RectangleGeometry(new Rect(1, 1, 10, 10))
                ));

        }
    }
}
