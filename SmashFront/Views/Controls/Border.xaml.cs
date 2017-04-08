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
    /// Interaction logic for Border.xaml
    /// </summary>
    public partial class Border : UserControl
    {
        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static readonly DependencyProperty BorderWidthProperty =
            DependencyProperty.Register("BorderWidth", typeof(double), typeof(IconGrid), new PropertyMetadata(1.0));

        public Border()
        {
            InitializeComponent();
            this.Loaded += Border_Loaded;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Line.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, BorderWidth, TimeSpan.FromSeconds(0.5)));
        }
    }
}
