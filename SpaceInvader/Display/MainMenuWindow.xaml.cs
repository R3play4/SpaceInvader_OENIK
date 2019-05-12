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
        public MainMenuWindow()
        {
            InitializeComponent();
            //public static ImageBrush UFO_3_Image = new ImageBrush(new BitmapImage(new Uri(Settings.UFO_3, UriKind.Relative)));
            //this.Background = new ImageBrush(new BitmapImage(new Uri(GlobalSettings.Settings.MainMenuBackground, UriKind.Relative)));
        }
        private void newGameBTN_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
