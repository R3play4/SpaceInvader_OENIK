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
    /// Interaction logic for PauseWindow.xaml
    /// </summary>
    public partial class PauseWindow : Window
    {
        SpaceInvaderLogic.GameLogic logic;

        public PauseWindow(SpaceInvaderLogic.GameLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files | *.xml";
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog() == true)
            {                
                this.logic.LoadGame(ofd.FileName);
            }
            this.DialogResult = true;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML Files | *.xml";
            sfd.DefaultExt = "xml";
            if (sfd.ShowDialog() == true)
            {
                this.logic.SaveGame(sfd.SafeFileName);
            }
            this.DialogResult = true;
        }

        private void ResumeButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
