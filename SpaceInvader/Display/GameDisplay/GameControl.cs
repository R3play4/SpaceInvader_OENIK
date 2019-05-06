using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using ClassRepository;
using GameLogic;


namespace Display.GameDisplay
{
    public class GameControl : FrameworkElement
    {
        GameLogic.GameLogic gameLogic;
        GameModel gameModel;
        GameDisplay gameDisplay;
        DispatcherTimer timer;

        public GameControl()
        {
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = Window.GetWindow(this);

            if (win != null)
            {
                gameModel = new GameModel();
                gameLogic = new GameLogic.GameLogic(gameModel);
                gameDisplay = new GameDisplay(gameModel);

                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(25);
                timer.Tick += Timer_Tick;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // UFO Mozgat
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Left)
            {
                // balra
            }
            else if (e.Key == System.Windows.Input.Key.Right)
            {
                // jobbra
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (gameDisplay != null)
            {
                gameDisplay.Display(drawingContext);
            }
        }
    }
}
