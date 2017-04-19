using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for PlayerWidget.xaml
    /// </summary>
    
    public enum PlayerTypes
    {
        Player,
        CPU,
        Empty
    };

    public partial class PlayerWidget : UserControl
    {
        private string _text = "";

        public PlayerTypes PlayerType
        {
            get { return (PlayerTypes)GetValue(PlayerTypeProperty); }
            set { SetValue(PlayerTypeProperty, value); }
        }

        public int Slot
        {
            get { return (int)GetValue(SlotProperty); }
            set { SetValue(SlotProperty, value); }
        }

        public static readonly DependencyProperty PlayerTypeProperty =
            DependencyProperty.Register("PlayerType", typeof(PlayerTypes), typeof(PlayerWidget), new PropertyMetadata(PlayerTypes.Empty));

        public static readonly DependencyProperty SlotProperty =
            DependencyProperty.Register("Slot", typeof(int), typeof(PlayerWidget), new PropertyMetadata(1));

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                TextBlock nameText = this.PanelCanvas.Children.OfType<TextBlock>().FirstOrDefault();
                if (nameText != null)
                {
                    nameText.Text = _text;

                    var formattedText = new FormattedText(_text, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight,
                        new Typeface(nameText.FontFamily, nameText.FontStyle, nameText.FontWeight, nameText.FontStretch),
                        nameText.FontSize, Brushes.Black);

                    Canvas.SetLeft(nameText, (this.Width - formattedText.Width) / 2);
                    Canvas.SetTop(nameText, (this.Height + formattedText.Height) / 2);
                }
            }
        }

        public PlayerWidget()
        {
            InitializeComponent();
            this.Loaded += PlayerWidget_Loaded;
        }

        private void PlayerWidget_Loaded(object sender, RoutedEventArgs e)
        {
            Rectangle topBorder = new Rectangle() { Height = 2, Fill = Brushes.Gray };
            this.PanelCanvas.Children.Add(topBorder);

            DoubleAnimation grow = new DoubleAnimation(0.0, this.Width, TimeSpan.FromSeconds(0.6));
            if (PlayerType != PlayerTypes.Empty)
            {
                grow.Completed += Grow_Completed;
            }
            topBorder.BeginAnimation(Rectangle.WidthProperty, grow);
        }

        private void Grow_Completed(object sender, EventArgs e)
        {
            TextBlock nameText = new TextBlock() { Name = "NameText", FontSize = 24, Foreground = Brushes.White, Opacity = 0.0 };
            this.PanelCanvas.Children.Add(nameText);

            Rectangle bottomBorder = new Rectangle() { Height = 2, Width = this.Width, Fill = Brushes.Gray };
            this.PanelCanvas.Children.Add(bottomBorder);

            DoubleAnimation move = new DoubleAnimation(0.0, 34.0, new Duration(TimeSpan.FromSeconds(0.4)));
            move.Completed += (s, args) =>
            {
                foreach (var child in PanelCanvas.Children)
                {
                    Rectangle border = child as Rectangle;
                    if (border != null)
                    {
                        if (PlayerType == PlayerTypes.Player)
                        {
                            border.Fill = Brushes.SlateGray;
                        }
                        else if (PlayerType == PlayerTypes.CPU)
                        {
                            border.Fill = Brushes.DarkSlateGray;
                        }
                    }

                    nameText.Opacity = 1.0;
                }
            };
            bottomBorder.BeginAnimation(Canvas.TopProperty, move);
        }
    }
}
