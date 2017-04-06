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

namespace SmashFront.Views.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public abstract partial class BasePage : Page
    {
        public virtual void FadedIn(object sender, EventArgs e) { }
        public virtual void FadedOut(object sender, EventArgs e) { }

        public void FadeIn(Double nSeconds)
        {
            DoubleAnimation fade = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(nSeconds)));
            fade.Completed += new EventHandler(FadedIn);
            this.BeginAnimation(Page.OpacityProperty, fade);
        }

        public void FadeOut(Double nSeconds)
        {
            DoubleAnimation fade = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(nSeconds)));
            fade.Completed += new EventHandler(FadedOut);
            this.BeginAnimation(Page.OpacityProperty, fade);
        }
    }
}
