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
using static SmashFront.ViewModels.Character;

namespace SmashFront.Views.Pages
{
    /// <summary>
    /// Interaction logic for CharacterSelect.xaml
    /// </summary>
    public partial class CharacterSelectPage : Page
    {
        private DispatcherTimer _timer;
        private bool _characterSelected;

        public CharacterSelectPage()
        {
            InitializeComponent();
            this.BeginAnimation(Page.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));

            this.Cursor = Cursors.None;
            this.Loaded += CharacterSelectPage_Loaded;

            CharacterIcons.IconNames = Character.GetCharacterNames();

            CharacterIcons.HoverTextChanged += (s, e) =>
            {
                P1Border.Text = e.Text;
            };

            CharacterIcons.SelectedTextChanged += CharacterSelected;
        }

        public void CharacterSelectPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = MainWindow.PreviousPageTitle;
            Selection.Text = MainWindow.PreviousPageTitle;

            double availableWidth = MenuGrid.ColumnDefinitions[1].ActualWidth - (P1Border.Margin.Left + P1Border.Margin.Right +
                                  P2Border.Margin.Left + P2Border.Margin.Right + P3Border.Margin.Left + P3Border.Margin.Right +
                                  P4Border.Margin.Left + P4Border.Margin.Right);
            P1Border.Width = availableWidth / 4;
            P2Border.Width = availableWidth / 4;
            P3Border.Width = availableWidth / 4;
            P4Border.Width = availableWidth / 4;

            double availableIconWidth = MenuGrid.ColumnDefinitions[1].ActualWidth - (CharacterIcons.GrowthAmount * 2 * CharacterIcons.ColumnCount);
            CharacterIcons.IconWidth = availableIconWidth / CharacterIcons.ColumnCount;
            CharacterIcons.IconHeight = availableIconWidth / CharacterIcons.ColumnCount;

            switch (MainWindow.PreviousPageTitle)
            {
                case "SINGLEPLAYER":
                    //Modes.Items.Add("CLASSIC");
                    //Modes.Items.Add("ADVENTURE");
                    //Modes.Items.Add("ALL STAR");
                    break;
                case "TRAINING":
                    //Modes.Items.Add("1-PLAYER");
                    break;
                case "LOCAL":
                    //Modes.Items.Add("SINGLES");
                    //Modes.Items.Add("DOUBLES");
                    var player = new MediaPlayer();
                    player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\Narrator\Narrator Modes\Melee.wav"));
                    player.Play();
                    break;
            }

            this.Cursor = Cursors.Pen;

            TopBorder.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, MenuGrid.ColumnDefinitions[1].ActualWidth, TimeSpan.FromSeconds(0.5)));
            BottomBorder.BorderWidth = MenuGrid.ColumnDefinitions[1].ActualWidth;

            this.MouseRightButtonDown += CharacterSelectPage_MouseRightButtonDown;
            this.MouseRightButtonUp += CharacterSelectPage_MouseRightButtonUp;
        }

        private void CharacterSelected(object sender, TextEventArgs e)
        {
            Characters character = Character.GetCharacterFromName(e.Text);
            string narratorName = Character.GetNarratorNameFromCharacter(character);

            if (!String.IsNullOrEmpty(narratorName))
            {
                var player = new MediaPlayer();
                player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\Narrator\Names\" + narratorName + ".wav"));
                player.Play();
            }

            this.Cursor = Cursors.None;
            _characterSelected = true;
            CharacterIcons.DisableControl();

            GameState state = new GameState();
            state.LaunchGame();
        }

        private void CharacterSelectPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GameState state = new GameState();
                state.LaunchGame();
            }
        }

        private void CharacterSelectPage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Pen;
            _characterSelected = false;
            CharacterIcons.EnableControl();
            CharacterIcons.Deselect();

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
