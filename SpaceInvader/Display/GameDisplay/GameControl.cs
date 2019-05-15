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
using SpaceInvaderLogic;
using SpaceInvaderLogic.Interfaces;
using Microsoft.Win32;

namespace Display.GameDisplay
{
    public class GameControl : FrameworkElement
    {
        SpaceInvaderLogic.GameLogic gameLogic;
        GameDisplay gameDisplay;
        DispatcherTimer ufoTimer;
        DispatcherTimer sidewayUfoTimer;
        DispatcherTimer projectileTimer;
        bool? isMovingRight;
        bool canShoot;

        //MainMenuWindow mainMenuWindow = new MainMenuWindow();
        //MainWindow window = new MainWindow();

        //public string MyProperty { get; set; }
        public GameControl()
        {
            //this.gameLogic = logic;
            //this.GameControl_Loaded();
            Loaded += GameControl_Loaded;
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        //private void GameControl_Loaded()
        {
            //mainMenuWindow.ShowDialog();

            //gameRepo = new GameRepository();
            // Display\bin\debug -> kell egy relative path a DefaultGameState Mappára. Vagy maradhat így.
            //gameModel = this.gameRepo.LoadGameState(GlobalSettings.Settings.GameStateXML);
            gameLogic = ((MainMenuWindow)Application.Current.MainWindow).Logic;
            gameDisplay = new GameDisplay(this.gameLogic.Model);
            //((MainMenuWindow)Application.Current.MainWindow).Logic
            

            Window win = Window.GetWindow(this);
            InvalidateVisual();

            if (win != null)
            {
                win.KeyDown += Win_KeyDown;
                win.KeyUp += Win_KeyUp;

                ufoTimer = new DispatcherTimer();
                ufoTimer.Interval = TimeSpan.FromMilliseconds(this.gameLogic.UfoTimerTick);
                ufoTimer.Tick += UfoTimer_Tick;
                ufoTimer.Start();

                sidewayUfoTimer = new DispatcherTimer();
                sidewayUfoTimer.Interval = TimeSpan.FromMilliseconds(100);
                sidewayUfoTimer.Tick += SideWayUFOTimer_Tick;
                sidewayUfoTimer.Start();

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
            else if (e.Key == Key.Space)
            {
                this.canShoot = true;
            }
        }

        private void ProjectileTimer_Tick(object sender, EventArgs e)
        {
            if (this.gameLogic.Model.GameState == GameState.Running)
            {
                this.gameLogic.ProjectileMove();

                for (int i = this.gameLogic.Model.Projectiles.Count - 1; i >= 0; i--)
                {
                    for (int j = this.gameLogic.Model.Shields.Count - 1; j >= 0; j--)
                    {
                        if (this.gameLogic.Model.Projectiles.Count > i &&
                            this.gameLogic.CollisionCheck(this.gameLogic.Model.Projectiles[i], this.gameLogic.Model.Shields[j]))
                        {
                            this.gameLogic.HandleCollision(this.gameLogic.Model.Projectiles[i], this.gameLogic.Model.Shields[j]);
                            this.gameLogic.DeathCheck(this.gameLogic.Model.Shields[j], typeof(Shield));
                        }
                    }

                    if (this.gameLogic.Model.Projectiles.Count > i &&
                        this.gameLogic.Model.Projectiles[i].SourceObject.GetType() == typeof(Player))
                    {
                        for (int k = this.gameLogic.Model.Ufos.Count - 1; k >= 0; k--)
                        {
                            if (this.gameLogic.Model.Projectiles.Count > i &&
                                this.gameLogic.CollisionCheck(this.gameLogic.Model.Projectiles[i], this.gameLogic.Model.Ufos[k]))
                            {
                                this.gameLogic.HandleCollision(this.gameLogic.Model.Projectiles[i], this.gameLogic.Model.Ufos[k]);
                                this.gameLogic.DeathCheck(this.gameLogic.Model.Ufos[k], typeof(Ufo));
                            }
                        }
                    }
                    else
                    {
                        if (this.gameLogic.Model.Projectiles.Count > i &&
                            this.gameLogic.CollisionCheck(this.gameLogic.Model.Projectiles[i], this.gameLogic.Model.Player))
                        {
                            this.gameLogic.HandleCollision(this.gameLogic.Model.Projectiles[i], this.gameLogic.Model.Player);
                            this.gameLogic.DeathCheck(this.gameLogic.Model.Player, typeof(Player));
                            if (this.gameLogic.Model.GameState == GameState.Finished)
                            {

                            }
                        }
                    }
                }

                if (this.gameLogic.CheckIfLevelCleared())
                {
                    ufoTimer.Interval = TimeSpan.FromMilliseconds(this.gameLogic.UfoTimerTick);
                }
                this.gameLogic.PlayerMove(this.isMovingRight);
                this.gameLogic.CleanupOffscreenProjectiles();
                InvalidateVisual();
            }            
        }

        private void UfoTimer_Tick(object sender, EventArgs e)
        {
            if (this.gameLogic.Model.GameState == GameState.Running)
            {
                this.gameLogic.UfoMove();
                this.gameLogic.UfoShoot();
                InvalidateVisual();
            }
        }

        private void SideWayUFOTimer_Tick(object sender, EventArgs e)
        {
            if (this.gameLogic.Model.GameState == GameState.Running)
            {
                gameLogic.UfoMoveSideways();
                InvalidateVisual();
            }
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && this.canShoot)
            {
                this.canShoot = false;
                this.gameLogic.PlayerShoot();
            }
            else if (e.Key == Key.Left)
            {
                this.isMovingRight = false;
            }
            else if (e.Key == Key.Right)
            {
                this.isMovingRight = true;
            }
            else if (e.Key == Key.Escape)
            {
                this.gameLogic.GameStateSwitch(GameState.Paused);
                PauseWindow window = new PauseWindow(this.gameLogic);
                window.ShowDialog();
                this.gameDisplay = new GameDisplay(this.gameLogic.Model);
                this.isMovingRight = null;
                this.gameLogic.GameStateSwitch(GameState.Running);
                InvalidateVisual();
            }
            else if (this.gameLogic.Model.GameState == GameState.Finished
                     && e.Key == Key.Enter)
            {
                Window.GetWindow(this).Close();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (gameDisplay != null)
            {
                if (this.gameLogic.Model.GameState == GameState.Finished)
                {
                    gameDisplay.DisplayGameOver(drawingContext);
                }
                else
                {
                    gameDisplay.Display(drawingContext);
                }                
            }
        }
    }
}
