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

namespace SmashFront.Views.Controls
{
    /// <summary>
    /// Interaction logic for MenuOptions.xaml
    /// </summary>
    public partial class MenuOptions : UserControl
    {
        private Arrow _selector = new Arrow(10.0);

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(IconGrid), new PropertyMetadata(""));

        public List<string> OptionNames { get; set; }

        public event MouseButtonEventHandler Selected;
        public event MouseEventHandler Entered;
        public event EventHandler Transitioned;
        public event EventHandler Shown;

        public MenuOptions()
        {
            InitializeComponent();
            OptionNames = new List<string>();

            this.Loaded += MenuOptions_Loaded;
        }

        public void MenuOptions_Loaded(object sender, RoutedEventArgs e)
        {
            NextTitle.Text = TitleText;
            NextTitle.BeginAnimation(TextBlock.OpacityProperty, new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.1)));

            for (Int32 i = 0; i < OptionNames.Count; i++)
            {
                TextBlock textBlock = new TextBlock() { Text = OptionNames[i], Foreground = Brushes.White, FontSize = 18, Opacity = 0.0, Margin = new Thickness(12, 4, 0, 0) };

                DoubleAnimation appear = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.1));
                appear.BeginTime = TimeSpan.FromSeconds(i * 0.1);

                if (i == OptionNames.Count - 1)
                {
                    appear.Completed += (s, args) => {
                        _selector.Opacity = 1.0;
                        Shown.Invoke(this, new EventArgs());
                    };
                }

                textBlock.BeginAnimation(TextBlock.OpacityProperty, appear);

                NextOptions.Children.Add(textBlock);
            }

            _selector.Opacity = 0.0;
            MenuOptions_Transitioned(this, new EventArgs());
        }

        public void TransitionForward(string title, IEnumerable<string> options)
        {
            NextTitle.Text = title;

            foreach (string option in options)
            {
                TextBlock textBlock = new TextBlock() { Text = option, Foreground = Brushes.White, FontSize = 18, Margin = new Thickness(12, 4, 0, 0) };
                NextOptions.Children.Add(textBlock);
            }

            (FindResource("TransitionForward") as Storyboard).Begin();
        }

        public void TransitionBackward(string title, IEnumerable<string> options)
        {
            NextTitle.Text = title;

            foreach (string option in options)
            {
                TextBlock textBlock = new TextBlock() { Text = option, Foreground = Brushes.White, FontSize = 18, Margin = new Thickness(12, 4, 0, 0) };
                NextOptions.Children.Add(textBlock);
            }

            (FindResource("TransitionBackward") as Storyboard).Begin();
        }

        public void MenuOptions_Transitioned(object sender, EventArgs e)
        {
            CurrentOptions.Children.Clear();

            for (Int32 i = NextOptions.Children.Count - 1; i >= 0; i--)
            {
                TextBlock textBlock = NextOptions.Children[i] as TextBlock;

                textBlock.MouseEnter += Option_MouseEnter;
                textBlock.MouseLeftButtonDown += Option_MouseLeftButtonDown;

                NextOptions.Children.Remove(textBlock);
                CurrentOptions.Children.Insert(0, textBlock);
            }

            CurrentTitle.Text = NextTitle.Text;
            TitleText = CurrentTitle.Text;

            DoubleAnimation set = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(0.0)));
            CurrentOptions.BeginAnimation(TextBlock.OpacityProperty, set);

            TextBlock option = Mouse.DirectlyOver as TextBlock;
            if (option != null && CurrentOptions.Children.Contains(option))
            {
                Option_MouseEnter(option, null);
            }

            Transitioned.Invoke(this, new EventArgs());
        }

        public void DisableControl()
        {
            // Remove the selector arrow icon
            if (this.OptionCanvas.Children.Contains(_selector))
            {
                this.OptionCanvas.Children.Remove(_selector);
            }

            // Remove all event handlers for navigating forwards or backwards
            foreach (var child in CurrentOptions.Children)
            {
                TextBlock textBlock = child as TextBlock;
                textBlock.MouseEnter -= Option_MouseEnter;
                textBlock.MouseLeftButtonDown -= Option_MouseLeftButtonDown;
            }
        }

        private void Option_MouseEnter(object sender, MouseEventArgs e)
        {
            Entered.Invoke(sender, e);

            if (!this.OptionCanvas.Children.Contains(_selector))
            {
                this.OptionCanvas.Children.Add(_selector);
            }

            TextBlock senderTextBlock = sender as TextBlock;

            Point location = senderTextBlock.TranslatePoint(new Point(0, 0), OptionCanvas);
            Point arrowLocation = _selector.TranslatePoint(new Point(0, 0), OptionCanvas);

            // Don't redraw the arrow if it's already where it should be
            if (location.Y + 8.0 != arrowLocation.Y)
            {
                _selector.BeginBounceAnimation(new Point(location.X - 20.0, location.Y + 8.0));
            }
        }

        private void Option_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Selected.Invoke(sender, e);
        }
    }
}
