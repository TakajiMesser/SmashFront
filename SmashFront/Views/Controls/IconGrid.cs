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
        private int _columnCount;
        private int _rowCount;
        private int _growth;
        private List<Rectangle> _boxes = new List<Rectangle>();
        private List<string> _iconNames = new List<string>();
        private List<ImageBrush> _images = new List<ImageBrush>();
        private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>();
        private List<RowDefinition> _rowDefinitions = new List<RowDefinition>();

        public int ColumnCount { get { return _columnCount; } }
        public int RowCount { get { return _rowCount; } }
        public int Growth { get { return _growth; } }
        public List<Rectangle> Boxes { get { return _boxes; } }
        public List<string> IconNames { get { return _iconNames; } }
        public List<ImageBrush> Images { get { return _images; } }
        public List<ColumnDefinition> ColumnDefinitions { get { return _columnDefinitions; } }
        public List<RowDefinition> RowDefinitions { get { return _rowDefinitions; } }

        public double IconWidth { get; set; }
        public double IconHeight { get; set; }

        public event EventHandler<TextEventArgs> HoverTextChanged;
        public event EventHandler<TextEventArgs> SelectedTextChanged;

        public IconGrid(int columnCount, int rowCount, int growth)
        {
            _columnCount = columnCount;
            _rowCount = rowCount;
            _growth = growth;

            for (var i = 0; i < _columnCount; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(100.0 / _columnCount, GridUnitType.Star);
                _columnDefinitions.Add(columnDefinition);
            }

            for (var i = 0; i < _rowCount; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(100.0 / _rowCount, GridUnitType.Star);
                _rowDefinitions.Add(rowDefinition);
            }
        }

        public void Activate()
        {
            for (var i = 0; i < IconNames.Count; i++)
            {
                ImageBrush icon = Images[i];

                DoubleAnimation fade = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.1));
                fade.BeginTime = TimeSpan.FromSeconds(0.20 + i * 0.05);
                icon.BeginAnimation(ImageBrush.OpacityProperty, fade);

                Rectangle box = new Rectangle()
                {
                    Stroke = Brushes.White,
                    StrokeThickness = 2,
                    Opacity = 0.0,
                    Width = IconWidth,
                    Height = IconHeight,
                    Fill = icon,
                    Name = IconNames[i]
                };
                box.Margin = new Thickness(0, _growth, 0, _growth);

                DoubleAnimation appear = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.05));
                appear.BeginTime = TimeSpan.FromSeconds(i * 0.05);
                box.BeginAnimation(Rectangle.OpacityProperty, appear);

                box.MouseEnter += Box_MouseEnter;
                box.MouseLeave += Box_MouseLeave;
                box.MouseLeftButtonDown += Box_MouseLeftButtonDown;

                Grid.SetRow(box, i % 2);
                Grid.SetColumn(box, i / 2);

                _boxes.Add(box);
            }
        }

        public void Deselect()
        {
            foreach (var box in _boxes)
            {
                if (box.ActualWidth > IconWidth || box.ActualHeight > IconHeight)
                {
                    box.BeginAnimation(Rectangle.MarginProperty, new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, _growth, 0, _growth), TimeSpan.FromSeconds(0.1)));
                    box.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(IconWidth + _growth, IconWidth, TimeSpan.FromSeconds(0.1)));
                    box.BeginAnimation(Rectangle.HeightProperty, new DoubleAnimation(IconHeight + _growth, IconHeight, TimeSpan.FromSeconds(0.1)));
                }
            }

            Rectangle selectedBox = Mouse.DirectlyOver as Rectangle;
            if (selectedBox != null && _boxes.Contains(selectedBox))
            {
                Box_MouseEnter(selectedBox, null);
            }
        }

        public void EnableControl()
        {
            foreach (var box in _boxes)
            {
                box.MouseEnter += Box_MouseEnter;
                box.MouseLeave += Box_MouseLeave;
                box.MouseLeftButtonDown += Box_MouseLeftButtonDown;
            }
        }

        public void DisableControl()
        {
            foreach (var box in _boxes)
            {
                box.MouseEnter -= Box_MouseEnter;
                box.MouseLeave -= Box_MouseLeave;
                box.MouseLeftButtonDown -= Box_MouseLeftButtonDown;
            }
        }

        private void Box_MouseEnter(object sender, EventArgs e)
        {
            Rectangle box = sender as Rectangle;
            HoverTextChanged.Invoke(this, new TextEventArgs(box.Name));

            box.BeginAnimation(Rectangle.MarginProperty, new ThicknessAnimation(new Thickness(0, _growth, 0, _growth), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(IconWidth, IconWidth + _growth, TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.HeightProperty, new DoubleAnimation(IconHeight, IconHeight + _growth, TimeSpan.FromSeconds(0.1)));
        }

        private void Box_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle box = sender as Rectangle;
            HoverTextChanged.Invoke(this, new TextEventArgs(""));

            box.BeginAnimation(Rectangle.MarginProperty, new ThicknessAnimation(new Thickness(0, 0, 0, 0), new Thickness(0, _growth, 0, _growth), TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(IconWidth + _growth, IconWidth, TimeSpan.FromSeconds(0.1)));
            box.BeginAnimation(Rectangle.HeightProperty, new DoubleAnimation(IconHeight + _growth, IconHeight, TimeSpan.FromSeconds(0.1)));
        }

        private void Box_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle box = sender as Rectangle;
            SelectedTextChanged.Invoke(this, new TextEventArgs(box.Name));
        }
    }

    public class TextEventArgs : EventArgs
    {
        private readonly string _text;

        public TextEventArgs(string text)
        {
            _text = text;
        }

        public string Text
        {
            get { return _text; }
        }
    }
}
