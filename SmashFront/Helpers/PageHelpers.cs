using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace SmashFront.Helpers
{
    public static class PageHelpers
    {
        public static void TransitionPage<T>(this Page currentPage, String title, Double nSeconds = 1.0) where T : Page, new()
        {
            currentPage.Title = title;

            DoubleAnimation fade = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(nSeconds)));
            fade.Completed += (s, args) =>
            {
                Page nextPage = new T();
                NavigationService.GetNavigationService(currentPage).Navigate(nextPage);
            };
            currentPage.BeginAnimation(Page.OpacityProperty, fade);
        }

        public static void ReenableCursor(this Page currentPage, object sender, EventArgs e)
        {
            currentPage.Cursor = Cursors.Pen;
        }

        public static void ShutDown(this Page currentPage)
        {
            DoubleAnimation fade = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(1.0)));
            fade.Completed += (s, args) => { Application.Current.Shutdown(); };
            currentPage.BeginAnimation(Frame.OpacityProperty, fade);
        }
    }
}
