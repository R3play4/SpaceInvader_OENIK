using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using ClassRepository;
using ClassRepository.Repository;
using GameLogic;


namespace Display.GameDisplay
{
    public class GameControl : FrameworkElement
    {
        GameLogic.GameLogic gameLogic;
        GameModel gameModel;
        GameDisplay gameDisplay;
        GameRepository gameRepo;
        DispatcherTimer ufoTimer;
        DispatcherTimer projectileTimer;

        public GameControl()
        {
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            gameRepo = new GameRepository();
            gameModel = this.gameRepo.LoadGameState("default.xml");
            gameLogic = new GameLogic.GameLogic(gameModel);
            gameDisplay = new GameDisplay(gameModel);

            Window win = Window.GetWindow(this);
            InvalidateVisual();

            if (win != null)
            {
                win.KeyDown += Win_KeyDown;

                ufoTimer = new DispatcherTimer();
                ufoTimer.Interval = TimeSpan.FromMilliseconds(1000);
                ufoTimer.Tick += UfoTimer_Tick;
                ufoTimer.Start();

                projectileTimer = new DispatcherTimer();
                projectileTimer.Interval = TimeSpan.FromMilliseconds(10);
                projectileTimer.Tick += ProjectileTimer_Tick;
                projectileTimer.Start();
            }
        }

        private void ProjectileTimer_Tick(object sender, EventArgs e)
        {
            this.gameLogic.ProjectileMove();
            InvalidateVisual();
        }

        private void UfoTimer_Tick(object sender, EventArgs e)
        {
            this.gameLogic.UfoMove();
            InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Left)
            {
                this.gameLogic.PlayerMove(-5);
            }
            else if (e.Key == System.Windows.Input.Key.Right)
            {
                this.gameLogic.PlayerMove(5);
            }
            else if (e.Key == System.Windows.Input.Key.Space)
            {
                this.gameLogic.PlayerShoot();
            }
            InvalidateVisual();
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
