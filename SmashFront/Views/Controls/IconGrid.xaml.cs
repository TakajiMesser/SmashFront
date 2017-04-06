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
    /// Interaction logic for CharacterGrid.xaml
    /// </summary>
    public partial class IconGrid : UserControl
    {
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        public int IconWidth
        {
            get { return (int)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public int IconHeight
        {
            get { return (int)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public int GrowthAmount
        {
            get { return (int)GetValue(GrowthAmountProperty); }
            set { SetValue(GrowthAmountProperty, value); }
        }

        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register("ColumnCount", typeof(int), typeof(IconGrid), new PropertyMetadata(1));

        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(IconGrid), new PropertyMetadata(1));

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(IconGrid), new PropertyMetadata(1));

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(int), typeof(IconGrid), new PropertyMetadata(1));

        public static readonly DependencyProperty GrowthAmountProperty =
            DependencyProperty.Register("GrowthAmount", typeof(int), typeof(IconGrid), new PropertyMetadata(1));

        public List<ImageBrush> Icons { get; set; }

        public IconGrid()
        {
            InitializeComponent();

            Icons = new List<ImageBrush>();
            this.Loaded += IconGrid_Loaded;
        }

        private void IconGrid_Loaded(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < ColumnCount; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(100.0 / ColumnCount, GridUnitType.Star);
                MainGrid.ColumnDefinitions.Add(columnDefinition);
            }

            for (var i = 0; i < RowCount; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(100.0 / RowCount, GridUnitType.Star);
                MainGrid.RowDefinitions.Add(rowDefinition);
            }

            for (var i = 0; i < Icons.Count; i++)
            {
                Rectangle box = new Rectangle();
                box.Stroke = Brushes.White;
                box.StrokeThickness = 2;
                box.Margin = new Thickness(0, GrowthAmount, 0, GrowthAmount);
                box.Opacity = 0.0;

                box.Width = IconWidth;
                box.Height = IconHeight;

                DoubleAnimation appear = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.05));
                appear.BeginTime = TimeSpan.FromSeconds(i * 0.05);
                box.BeginAnimation(Rectangle.OpacityProperty, appear);

                box.Fill = Icons[i];

                box.MouseEnter += Box_MouseEnter;
                box.MouseLeave += Box_MouseLeave;

                DoubleAnimation fade = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.1));
                fade.BeginTime = TimeSpan.FromSeconds(0.20 + i * 0.05);
                Icons[i].BeginAnimation(ImageBrush.OpacityProperty, fade);

                Grid.SetRow(box, i % 2);
                Grid.SetColumn(box, i / 2);

                MainGrid.Children.Add(box);
            }
        }

        public void Box_MouseEnter(object sender, EventArgs e)
        {
            Rectangle box = sender as Rectangle;

            box.BeginAnimation(Rectangle.MarginProperty, new ThicknessAnimation(new Thickness(0, GrowthAmount, 0, GrowthAmount), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(IconWidth, IconWidth + GrowthAmount, TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.HeightProperty, new DoubleAnimation(IconHeight, IconHeight + GrowthAmount, TimeSpan.FromSeconds(0.1)));
        }

        private void Box_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle box = sender as Rectangle;

            box.BeginAnimation(Rectangle.MarginProperty, new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, GrowthAmount, 0, GrowthAmount), TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(IconWidth + GrowthAmount, IconWidth, TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.HeightProperty, new DoubleAnimation(IconHeight + GrowthAmount, IconHeight, TimeSpan.FromSeconds(0.1)));
        }
    }
}
