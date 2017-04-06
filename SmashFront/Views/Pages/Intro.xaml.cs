using SmashFront.Helpers;
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
    /// Interaction logic for Intro.xaml
    /// </summary>
    public partial class Intro : Page
    {
        public Intro()
        {
            InitializeComponent();
            this.BeginAnimation(Page.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(1.0))));

            this.Cursor = Cursors.None;

            this.KeyDown += new KeyEventHandler(Intro_KeyPressed);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Intro_MouseClick);
            this.IntroVideo.MediaEnded += new RoutedEventHandler(Intro_VideoEnded);
            this.Loaded += Intro_Loaded;
        }

        private void Intro_Loaded(object sender, RoutedEventArgs e)
        {
            this.IntroVideo.Play();

            Storyboard story = new Storyboard();
            Storyboard.SetTargetProperty(story, new PropertyPath(TextBox.OpacityProperty));

            DoubleAnimation flash = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(1)));
            flash.AutoReverse = true;
            flash.RepeatBehavior = RepeatBehavior.Forever;

            story.Children.Add(flash);
            story.Begin(Prompt);
        }

        public void Intro_KeyPressed(object sender, KeyEventArgs e)
        {
            this.TransitionPage<MenuPage>(this.Title);
        }

        public void Intro_MouseClick(object sender, MouseButtonEventArgs e)
        {
            this.TransitionPage<MenuPage>(this.Title);
        }

        public void Intro_VideoEnded(object sender, RoutedEventArgs e)
        {
            IntroVideo.Position = TimeSpan.Zero;
            IntroVideo.Play();
        }
    }
}
