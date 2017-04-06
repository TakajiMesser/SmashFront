using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SmashFront.Views.Controls
{
    public class MenuCursor : FrameworkElement
    {
        /*public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(Double), typeof(Arrow));

        public MenuCursor(Double size)
        {
            Size = size;
            this.Fill = Brushes.White;
            this.Stroke = Brushes.White;
            this.StrokeThickness = 1;
        }

        public Double Size
        {
            get { return (Double)this.GetValue(SizeProperty); }
            set { this.SetValue(SizeProperty, value); }
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                Point p1 = new Point(0.0, 0.0);
                Point p2 = new Point(0.0, Size);
                Point p3 = new Point(Size / 2, Size / 2);

                List<PathSegment> segments = new List<PathSegment>(3);
                segments.Add(new LineSegment(p1, true));
                segments.Add(new LineSegment(p2, true));
                segments.Add(new LineSegment(p3, true));

                PathFigure figure = new PathFigure(p1, segments, true);
                List<PathFigure> figures = new List<PathFigure>() { figure };

                Geometry geometry = new PathGeometry(figures, FillRule.EvenOdd, null);
                //geometry.Freeze();
                return geometry;
            }
        }*/
    }
}
