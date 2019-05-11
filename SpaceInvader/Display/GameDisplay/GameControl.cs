using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        bool? isMovingRight;

        public GameControl()
        {
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            gameRepo = new GameRepository();

            // Display\bin\debug -> kell egy relative path a DefaultGameState Mappára. Vagy maradhat így.
            gameModel = this.gameRepo.LoadGameState("..\\..\\DefaultGameState\\default.xml");
            gameLogic = new GameLogic.GameLogic(gameModel);
            gameDisplay = new GameDisplay(gameModel);

            Window win = Window.GetWindow(this);
            InvalidateVisual();

            if (win != null)
            {
                win.KeyDown += Win_KeyDown;
                win.KeyUp += Win_KeyUp;

                ufoTimer = new DispatcherTimer();
                ufoTimer.Interval = TimeSpan.FromMilliseconds(1000);
                ufoTimer.Tick += UfoTimer_Tick;
                ufoTimer.Start();

                projectileTimer = new DispatcherTimer();
                projectileTimer.Interval = TimeSpan.FromMilliseconds(1);
                projectileTimer.Tick += ProjectileTimer_Tick;
                projectileTimer.Start();
            }
        }

        private void Win_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Left && this.isMovingRight == false) ||
                 e.Key == Key.Right && this.isMovingRight == true)
            {
                this.isMovingRight = null;
            }
        }

        private void ProjectileTimer_Tick(object sender, EventArgs e)
        {
            this.gameLogic.ProjectileMove();

            for (int i = this.gameLogic.model.Projectiles.Count - 1; i >= 0; i--)
            {
                for (int j = this.gameLogic.model.Shields.Count - 1; j >= 0; j--)
                {
                    if (this.gameLogic.model.Projectiles.Count > i && // Lehet mindig igaz ? 
                        this.gameLogic.CollisionCheck(this.gameLogic.model.Projectiles[i], this.gameLogic.model.Shields[j]))
                    {
                        this.gameLogic.HandleCollision(this.gameLogic.model.Projectiles[i], this.gameLogic.model.Shields[j]);
                        this.gameLogic.DeathCheck(this.gameLogic.model.Shields[j], typeof(Shield));
                    }
                }

                if (this.gameLogic.model.Projectiles.Count > i &&
                    this.gameLogic.model.Projectiles[i].SourceObject.GetType() == typeof(Player))
                {
                    for (int k = this.gameLogic.model.UFOs.Count - 1; k >= 0; k--)
                    {
                        if (this.gameLogic.model.Projectiles.Count > i &&
                            this.gameLogic.CollisionCheck(this.gameLogic.model.Projectiles[i], this.gameLogic.model.UFOs[k]))
                        {
                            this.gameLogic.HandleCollision(this.gameLogic.model.Projectiles[i], this.gameLogic.model.UFOs[k]);
                            this.gameLogic.DeathCheck(this.gameLogic.model.UFOs[k], typeof(UFO));
                        }
                    }
                }
                else
                {
                    if (this.gameLogic.model.Projectiles.Count > i &&
                        this.gameLogic.CollisionCheck(this.gameLogic.model.Projectiles[i], this.gameLogic.model.Player))
                    {
                        this.gameLogic.HandleCollision(this.gameLogic.model.Projectiles[i], this.gameLogic.model.Player);
                        this.gameLogic.DeathCheck(this.gameLogic.model.Player, typeof(Player));
                    }
                }
            }

            this.gameLogic.PlayerMove(this.isMovingRight);
            this.gameLogic.CleanupOffscreenProjectiles();
            InvalidateVisual();
        }

        private void UfoTimer_Tick(object sender, EventArgs e)
        {
            this.gameLogic.UfoMove();
            this.gameLogic.UfoShoot();
            InvalidateVisual();
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                this.gameLogic.PlayerShoot();
            }
            else if (e.Key == System.Windows.Input.Key.Left)
            {
                this.isMovingRight = false;
            }
            else if (e.Key == System.Windows.Input.Key.Right)
            {
                this.isMovingRight = true;
            }
            //InvalidateVisual();
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
