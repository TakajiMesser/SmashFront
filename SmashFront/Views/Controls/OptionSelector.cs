using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SmashFront.Views.Controls
{
    public class OptionSelector : ListBox
    {
        //public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(Double), typeof(Arrow));

        public OptionSelector()
        {
            // The canvas supports absolute positioning
            FrameworkElementFactory panel = new FrameworkElementFactory(typeof(Canvas));
            this.ItemsPanel = new ItemsPanelTemplate(panel);

            // Tells the container where to position the items
            this.ItemContainerStyle = new Style();
            this.ItemContainerStyle.Setters.Add(new Setter(Canvas.LeftProperty, new Binding("X")));
            this.ItemContainerStyle.Setters.Add(new Setter(Canvas.TopProperty, new Binding("Y")));

            this.Background = Brushes.Transparent;

            this.MinWidth = 30;
        }

        /*<StackPanel Background = "Red" >
            < ListBox Background="Transparent" BorderBrush="Transparent">
                <ListBox.Resources>
                    <Style TargetType = "{x:Type ListBoxItem}" >
                        < Setter Property="Background" Value="White" />
                        <Setter Property = "Margin" Value="1" />
                    </Style>
                </ListBox.Resources>
                <ListBoxItem Content = "First Item" />
                < ListBoxItem Content="Secton Item"/>
            </ListBox>
        </StackPanel>*/

        public OptionSelector(params String[] options)
        {
            //FontSize = "14" Opacity = "0.8" Foreground = "White"
            foreach (String option in options)
            {
                this.Items.Add(option);
            }
        }
    }
}
