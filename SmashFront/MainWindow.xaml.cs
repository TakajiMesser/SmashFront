using SmashFront.Helpers;
using SmashFront.Models;
using SmashFront.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmashFront
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer _player = new MediaPlayer();
        public static String PreviousPageTitle { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            /*GameState state = new GameState();
            state.LaunchGame();

            Application.Current.Shutdown();
            return;*/

            MainFrame.NavigationService.Navigating += MainFrame_Navigating;
            MainFrame.NavigationService.Navigated += NavigationService_Navigated;
            this.KeyDown += MainWindow_KeyDown;

            Page intro = new Views.Pages.Intro();
            MainFrame.NavigationService.Navigate(intro);

            _player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\Menu1.wav"));
        }

        private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Page previousPage = MainFrame.NavigationService.Content as Page;
            if (previousPage != null)
            {
                PreviousPageTitle = previousPage.Title;

                /*if (previousPage.Title == "Intro")
                {
                    _player.MediaEnded += (s, args) =>
                    {
                        _player.Position = TimeSpan.Zero;
                        _player.Play();
                    };
                    _player.Play();
                }
                else
                {
                    _player.Stop();
                }*/
            }
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            //if (MainFrame.NavigationService.Content != )
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                (MainFrame.NavigationService.Content as Page).ShutDown();
            }
        }
    }
}
