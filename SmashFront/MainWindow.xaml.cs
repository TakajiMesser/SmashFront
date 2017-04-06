using SmashFront.Helpers;
using SmashFront.Views.Pages;
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
        public static String PreviousPageTitle { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            MainFrame.NavigationService.Navigating += MainFrame_Navigating;
            this.KeyDown += MainWindow_KeyDown;

            Page intro = new Views.Pages.Intro();
            MainFrame.NavigationService.Navigate(intro);
        }

        protected void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Page previousPage = MainFrame.NavigationService.Content as Page;
            if (previousPage != null)
            {
                PreviousPageTitle = previousPage.Title;
            }
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
