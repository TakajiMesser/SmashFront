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

using SmashFront.Views.Controls;
using SmashFront.Helpers;
using SmashFront.ViewModels;
using System.Media;

namespace SmashFront.Views.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            this.BeginAnimation(Page.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));

            this.Cursor = Cursors.None;
            this.Loaded += MenuPage_Loaded;

            Options.Shown += this.ReenableCursor;
            Options.Entered += (s, args) => { OptionDescription.Text = MenuScreen.GetDescriptionForOption((s as TextBlock).Text); };
            Options.Selected += Options_Selected;
            Options.Transitioned += (s, args) => { MenuGrid.MouseRightButtonDown += MenuPage_MouseRightButtonDown; };
        }

        public void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            MenuScreenHeaders currentHeader;

            switch (MainWindow.PreviousPageTitle)
            {
                case "Intro":
                case "TRAINING":
                    currentHeader = MenuScreenHeaders.MainMenu;
                    break;
                case "LOCAL":
                    currentHeader = MenuScreenHeaders.Multiplayer;
                    break;
                default:
                    return;
            }

            Options.TitleText = MenuScreen.GetTitleForHeader(currentHeader);
            Options.OptionNames = MenuScreen.GetOptionsForHeader(currentHeader);

            String imageName = MenuScreen.GetImageNameForHeader(currentHeader);
            MenuGrid.Background = FindResource(imageName) as ImageBrush;

            BottomBorder.BorderWidth = MenuGrid.ColumnDefinitions[1].ActualWidth;
        }

        public void Options_Selected(object sender, MouseButtonEventArgs e)
        {
            MenuScreenHeaders currentHeader;
            TextBlock senderTextBlock = sender as TextBlock;

            switch (senderTextBlock.Text)
            {
                case "SINGLEPLAYER":
                    {
                        var player = new MediaPlayer();
                        player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\General\GENERAL SOUND EFFECTS\harp.wav"));
                        player.Play();
                    }
                    
                    currentHeader = MenuScreenHeaders.Singleplayer;
                    break;

                case "MULTIPLAYER":
                    {
                        var player = new MediaPlayer();
                        player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\General\GENERAL SOUND EFFECTS\harp.wav"));
                        player.Play();
                    }

                    currentHeader = MenuScreenHeaders.Multiplayer;
                    break;

                case "STADIUM":
                    currentHeader = MenuScreenHeaders.Stadium;
                    break;

                case "TRAINING":
                    Options.DisableControl();
                    this.TransitionPage<CharacterSelectPage>("TRAINING", 0.5);
                    return;

                case "SETTINGS":
                    currentHeader = MenuScreenHeaders.Settings;
                    break;

                case "LOCAL":
                    Options.DisableControl();
                    this.TransitionPage<CharacterSelectPage>("LOCAL", 0.5);
                    return;

                case "QUIT":
                    Options.DisableControl();
                    this.ShutDown();
                    return;

                default:
                    OptionDescription.Text = "Not yet implemented!";
                    return;
            }

            PrepareForTransition(currentHeader);
            Options.TransitionForward(MenuScreen.GetTitleForHeader(currentHeader), MenuScreen.GetOptionsForHeader(currentHeader));
        }

        public void MenuPage_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MenuScreenHeaders currentHeader;

            switch (Options.TitleText)
            {
                case "MAIN MENU":
                    Options.DisableControl();
                    this.TransitionPage<Intro>(this.Title);
                    return;

                case "SINGLEPLAYER":
                case "MULTIPLAYER":
                case "STADIUM":
                case "TRAINING":
                case "SETTINGS":
                    currentHeader = MenuScreenHeaders.MainMenu;
                    break;

                default:
                    return;
            }

            PrepareForTransition(currentHeader);
            Options.TransitionBackward(MenuScreen.GetTitleForHeader(currentHeader), MenuScreen.GetOptionsForHeader(currentHeader));
        }

        public void PrepareForTransition(MenuScreenHeaders currentHeader)
        {
            this.OptionDescription.Text = "";
            MenuGrid.MouseRightButtonDown -= MenuPage_MouseRightButtonDown;

            DoubleAnimation fade = new DoubleAnimation(0.6, 0.0, TimeSpan.FromSeconds(0.3));
            fade.Completed += (s, args) =>
            {
                String imageName = MenuScreen.GetImageNameForHeader(currentHeader);
                MenuGrid.Background = FindResource(imageName) as ImageBrush;
                MenuGrid.Background.BeginAnimation(ImageBrush.OpacityProperty, new DoubleAnimation(0.0, 0.6, TimeSpan.FromSeconds(0.3)));
            };
            MenuGrid.Background.BeginAnimation(ImageBrush.OpacityProperty, fade);
        }
    }
}
