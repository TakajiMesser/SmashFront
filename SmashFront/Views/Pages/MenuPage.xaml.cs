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

namespace SmashFront.Views.Pages
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        private Arrow _selector = new Arrow(10.0);
        private MenuScreenHeaders _currentHeader;

        public MenuPage()
        {
            InitializeComponent();
            this.BeginAnimation(Page.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));

            this.Cursor = Cursors.None;
            this.Loaded += MenuPage_Loaded;
        }

        public void MenuPage_Loaded(object sender, RoutedEventArgs e)
        {
            switch (MainWindow.PreviousPageTitle)
            {
                case "Intro":
                case "TRAINING":
                    _currentHeader = MenuScreenHeaders.MainMenu;
                    break;
                case "LOCAL":
                    _currentHeader = MenuScreenHeaders.Multiplayer;
                    break;
                default:
                    return;
            }

            NextTitle.Text = MenuScreen.GetNameForHeader(_currentHeader); ;
            List<String> options = MenuScreen.GetOptionsForHeader(_currentHeader);
            String imageName = MenuScreen.GetImageNameForHeader(_currentHeader);

            MenuGrid.Background = FindResource(imageName) as ImageBrush;

            NextTitle.BeginAnimation(TextBlock.OpacityProperty, new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.1)));

            for (Int32 i = 0; i < options.Count; i++)
            {
                TextBlock textBlock = new TextBlock() { Text = options[i], Foreground = Brushes.White, FontSize = 18, Opacity = 0.0 };
                textBlock.Margin = new Thickness(12, 4, 0, 0);

                DoubleAnimation appear = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.1));
                appear.BeginTime = TimeSpan.FromSeconds(i * 0.1);

                if (i == options.Count - 1)
                {
                    appear.Completed += new EventHandler(this.ReenableCursor);
                    appear.Completed += (s, args) => { _selector.Opacity = 1.0; };
                }

                textBlock.BeginAnimation(TextBlock.OpacityProperty, appear);

                NextOptions.Children.Add(textBlock);
            }

            BottomBorder.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, MenuGrid.ColumnDefinitions[1].ActualWidth, TimeSpan.FromSeconds(0.5)));

            _selector.Opacity = 0.0;
            MenuPage_Transitioned(this, null);
        }

        public void MenuPage_Transitioned(object sender, EventArgs e)
        {
            CurrentOptions.Children.Clear();

            for (Int32 i = NextOptions.Children.Count - 1; i >= 0; i--)
            {
                TextBlock textBlock = NextOptions.Children[i] as TextBlock;

                textBlock.MouseEnter += new MouseEventHandler(OnOptionEnter);
                textBlock.MouseLeftButtonDown += new MouseButtonEventHandler(OnOptionSelected);

                NextOptions.Children.Remove(textBlock);
                CurrentOptions.Children.Insert(0, textBlock);
            }

            CurrentTitle.Text = NextTitle.Text;
            DoubleAnimation set = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(0.0)));
            CurrentOptions.BeginAnimation(TextBlock.OpacityProperty, set);

            TextBlock option = Mouse.DirectlyOver as TextBlock;
            if (option != null && CurrentOptions.Children.Contains(option))
            {
                OnOptionEnter(option, null);
            }

            MenuGrid.MouseRightButtonDown += new MouseButtonEventHandler(OnRightClick);
        }

        public void OnOptionSelected(object sender, MouseButtonEventArgs e)
        {
            switch ((sender as TextBlock).Text)
            {
                case "SINGLEPLAYER":
                    _currentHeader = MenuScreenHeaders.Singleplayer;
                    break;

                case "MULTIPLAYER":
                    _currentHeader = MenuScreenHeaders.Multiplayer;
                    break;

                case "STADIUM":
                    _currentHeader = MenuScreenHeaders.Stadium;
                    break;

                case "TRAINING":
                    DisableControl();
                    this.TransitionPage<CharacterSelectPage>("TRAINING", 0.5);
                    return;

                case "SETTINGS":
                    _currentHeader = MenuScreenHeaders.Settings;
                    break;

                case "LOCAL":
                    DisableControl();
                    this.TransitionPage<CharacterSelectPage>("LOCAL", 0.5);
                    return;

                case "QUIT":
                    DisableControl();
                    this.ShutDown();
                    return;

                default:
                    OptionDescription.Text = "Not yet implemented!";
                    return;
            }

            DisableControl();

            PrepareNextPage();
            (FindResource("TransitionForward") as Storyboard).Begin();
        }

        public void OnRightClick(object sender, MouseButtonEventArgs e)
        {
            DisableControl();

            switch (CurrentTitle.Text)
            {
                case "MAIN MENU":
                    this.TransitionPage<Intro>(this.Title);
                    return;

                case "SINGLEPLAYER":
                case "MULTIPLAYER":
                case "STADIUM":
                case "TRAINING":
                case "SETTINGS":
                    _currentHeader = MenuScreenHeaders.MainMenu;
                    break;

                default:
                    return;
            }

            PrepareNextPage();
            (FindResource("TransitionBackward") as Storyboard).Begin();
        }

        public void DisableControl()
        {
            // Remove the selector arrow icon
            if (this.OptionCanvas.Children.Contains(_selector))
            {
                this.OptionCanvas.Children.Remove(_selector);
            }

            // Remove the description text
            this.OptionDescription.Text = "";

            // Remove all event handlers for navigating forwards or backwards
            MenuGrid.MouseRightButtonDown -= OnRightClick;
            foreach (var child in CurrentOptions.Children)
            {
                TextBlock textBlock = child as TextBlock;
                textBlock.MouseEnter -= OnOptionEnter;
                textBlock.MouseLeftButtonDown -= OnOptionSelected;
            }
        }

        public void PrepareNextPage()
        {
            NextTitle.Text = MenuScreen.GetNameForHeader(_currentHeader);

            DoubleAnimation fade = new DoubleAnimation(0.6, 0.0, TimeSpan.FromSeconds(0.3));
            fade.Completed += (s, args) =>
            {
                String imageName = MenuScreen.GetImageNameForHeader(_currentHeader);
                MenuGrid.Background = FindResource(imageName) as ImageBrush;
                MenuGrid.Background.BeginAnimation(ImageBrush.OpacityProperty, new DoubleAnimation(0.0, 0.6, TimeSpan.FromSeconds(0.3)));
            };
            MenuGrid.Background.BeginAnimation(ImageBrush.OpacityProperty, fade);

            foreach (String option in MenuScreen.GetOptionsForHeader(_currentHeader))
            {
                TextBlock textBlock = new TextBlock();

                textBlock.Text = option;
                textBlock.Foreground = Brushes.White;
                textBlock.Margin = new Thickness(12, 4, 0, 0);
                textBlock.FontSize = 18;

                NextOptions.Children.Add(textBlock);
            }
        }

        public void OnOptionEnter(object sender, MouseEventArgs e)
        {
            if (!this.OptionCanvas.Children.Contains(_selector))
            {
                this.OptionCanvas.Children.Add(_selector);
            }

            TextBlock senderTextBlock = sender as TextBlock;
            OptionDescription.Text = MenuScreen.GetDescriptionForOption(senderTextBlock.Text);

            Point location = senderTextBlock.TranslatePoint(new Point(0, 0), OptionCanvas);
            Point arrowLocation = _selector.TranslatePoint(new Point(0, 0), OptionCanvas);

            // Don't redraw the arrow if it's already where it should be
            if (location.Y + 8.0 != arrowLocation.Y)
            {
                _selector.BeginBounceAnimation(new Point(location.X - 20.0, location.Y + 8.0));
            }
        }
    }
}
