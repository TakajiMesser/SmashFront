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

namespace SmashFront.Views.Pages
{
    /// <summary>
    /// Interaction logic for CharacterSelect.xaml
    /// </summary>
    public partial class CharacterSelectPage : Page
    {
        public readonly String[] CharacterNames = { "Bowser", "DonkeyKong", "DrMario", "Falco", "Falcon",
                                                    "Fox", "GameAndWatch", "Ganon", "IceClimbers", "Jigglypuff",
                                                    "Kirby", "Link", "Luigi", "Mario", "Marth",
                                                    "Mewtwo", "Ness", "Peach", "Pichu", "Pikachu",
                                                    "Roy", "Samus", "Yoshi", "YoungLink", "Zelda",
                                                    "Random" };

        private List<ImageBrush> _icons = new List<ImageBrush>();

        public CharacterSelectPage()
        {
            InitializeComponent();
            this.BeginAnimation(Page.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));

            this.Cursor = Cursors.None;
            this.Loaded += new RoutedEventHandler(OnLoaded);

            foreach (String name in CharacterNames)
            {
                ImageBrush image = FindResource(name) as ImageBrush;
                CharacterIcons.Icons.Add(image);
            }
        }

        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Title = MainWindow.PreviousPageTitle;
            Selection.Text = MainWindow.PreviousPageTitle;

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
                    break;
            }

            for (Int32 i = 1; i <= 99; i++)
            {
                Stocks.Items.Add(i);
            }
            for (Int32 i = 1; i <= 20; i++)
            {
                TimeLimit.Items.Add(i + " MINUTES");
            }
            Items.Items.Add("Something");

            //Modes.SelectedIndex = 0;
            Stocks.SelectedIndex = 0;
            TimeLimit.SelectedIndex = 0;
            Items.SelectedIndex = 0;

            this.Cursor = Cursors.Pen;

            TopBorder.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, MenuGrid.ColumnDefinitions[1].ActualWidth, TimeSpan.FromSeconds(0.5)));
            BottomBorder.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, MenuGrid.ColumnDefinitions[1].ActualWidth, TimeSpan.FromSeconds(0.5)));

            MenuGrid.MouseRightButtonDown += new MouseButtonEventHandler(OnRightClick);
        }

        public void OnRightClick(object sender, MouseButtonEventArgs e)
        {
            this.TransitionPage<MenuPage>(this.Title);
        }
    }
}
