using SmashFront.Helpers;
using SmashFront.Models;
using SmashFront.ViewModels;
using SmashFront.Views.Controls;
using SmashFront.Views.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using System.Windows.Threading;

namespace SmashFront.Views.Pages
{
    /// <summary>
    /// Interaction logic for CharacterSelect.xaml
    /// </summary>
    public partial class TrainingPage : Page
    {
        private DispatcherTimer _timer;

        public TrainingPage()
        {
            InitializeComponent();
            this.BeginAnimation(Page.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));

            this.Cursor = Cursors.None;
            this.Loaded += CharacterSelectPage_Loaded;

            CharacterGrid.IconGrid.HoverTextChanged += (s, e) =>
            {
                P1Border.Text = e.Text;
            };
        }

        public void CharacterSelectPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = MainWindow.PreviousPageTitle;
            Selection.Text = MainWindow.PreviousPageTitle;

            double availableWidth = MenuGrid.ColumnDefinitions[1].ActualWidth - (P1Border.Margin.Left + P1Border.Margin.Right /*+
                                  P2Border.Margin.Left + P2Border.Margin.Right + P3Border.Margin.Left + P3Border.Margin.Right +
                                  P4Border.Margin.Left + P4Border.Margin.Right*/);
            P1Border.Width = availableWidth / 4;
            /*P2Border.Width = availableWidth / 4;
            P3Border.Width = availableWidth / 4;
            P4Border.Width = availableWidth / 4;*/

            double availableIconWidth = MenuGrid.ColumnDefinitions[1].ActualWidth - (CharacterGrid.IconGrid.Growth * 2 * CharacterGrid.IconGrid.ColumnCount);
            CharacterGrid.IconGrid.IconWidth = availableIconWidth / CharacterGrid.IconGrid.ColumnCount;
            CharacterGrid.IconGrid.IconHeight = availableIconWidth / CharacterGrid.IconGrid.ColumnCount;

            var player = new MediaPlayer();
            player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\Narrator\Narrator Modes\Melee.wav"));
            player.Play();

            this.Cursor = Cursors.Pen;

            TopBorder.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, MenuGrid.ColumnDefinitions[1].ActualWidth, TimeSpan.FromSeconds(0.5)));
            BottomBorder.BorderWidth = MenuGrid.ColumnDefinitions[1].ActualWidth;

            this.MouseRightButtonDown += CharacterSelectPage_MouseRightButtonDown;
            this.MouseRightButtonUp += CharacterSelectPage_MouseRightButtonUp;
        }

        private void CharacterSelectPage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Pen;
            CharacterGrid.IconGrid.EnableControl();
            CharacterGrid.IconGrid.Deselect();

            _timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(2) };
            _timer.Tick += (s, args) =>
            {
                this.TransitionPage<MenuPage>(this.Title);
            };

            _timer.Start();
        }

        private void CharacterSelectPage_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _timer.Stop();
        }
    }
}
