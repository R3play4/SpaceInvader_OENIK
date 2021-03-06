﻿using ClassRepository;
using ClassRepository.Repository;
using Display.GameDisplay;
using GlobalSettings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Display
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public SpaceInvaderLogic.GameLogic Logic { get; set; }

        public MainMenuWindow()
        {
            InitializeComponent();
        }
        private void newGameBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Logic = new SpaceInvaderLogic.GameLogic(Settings.GameStateXML);
            MainWindow window = new MainWindow();
            window.ShowDialog();
        }

        private void LoadGameBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if(ofd.ShowDialog() == true)
            {
                this.Logic = new SpaceInvaderLogic.GameLogic(ofd.FileName);
                //GlobalSettings.Settings.GameStateXML = ofd.FileName;
            }

            MainWindow window = new MainWindow();
            window.ShowDialog();

        }

        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
