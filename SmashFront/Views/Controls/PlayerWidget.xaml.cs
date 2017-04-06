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

        public PlayerWidget()
        {
            InitializeComponent();

            /* double marginWidth = P1Border.Margin.Left + P1Border.Margin.Right + 
                                 P2Border.Margin.Left + P2Border.Margin.Right + 
                                 P3Border.Margin.Left + P3Border.Margin.Right + 
                                 P4Border.Margin.Left + P4Border.Margin.Right;
            double availableWidth = MenuGrid.ColumnDefinitions[1].ActualWidth - marginWidth;

            P1Border.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, availableWidth / 4, TimeSpan.FromSeconds(0.8)));
            P2Border.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, availableWidth / 4, TimeSpan.FromSeconds(0.8)));
            P3Border.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, availableWidth / 4, TimeSpan.FromSeconds(0.8)));
            P4Border.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(0.0, availableWidth / 4, TimeSpan.FromSeconds(0.8)));*/
        }
    }
}
