using SmashFront.Models;
using SmashFront.ViewModels;
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
using static SmashFront.ViewModels.Character;

namespace SmashFront.Views.Controls
{
    /// <summary>
    /// Interaction logic for CSS.xaml
    /// </summary>
    public partial class CSS : UserControl
    {
        /*<local:IconGrid x:Name="CharacterIcons" Grid.Row="3" Grid.Column="1"
                        HorizontalAlignment="Stretch" GrowthAmount="10"
                        RowCount="2" ColumnCount="13"></local:IconGrid>*/
        private IconGrid _iconGrid = new IconGrid(13, 2, 10);

        public IconGrid IconGrid { get { return _iconGrid; } }

        public CSS()
        {
            InitializeComponent();
            this.Loaded += CSS_Loaded;
            _iconGrid.SelectedTextChanged += CharacterSelected;
        }

        private void CSS_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var columnDefinition in _iconGrid.ColumnDefinitions)
            {
                MainGrid.ColumnDefinitions.Add(columnDefinition);
            }

            foreach (var rowDefinition in _iconGrid.RowDefinitions)
            {
                MainGrid.RowDefinitions.Add(rowDefinition);
            }

            foreach (var name in Character.GetCharacterNames())
            {
                _iconGrid.IconNames.Add(name);
                _iconGrid.Images.Add(FindResource(name) as ImageBrush);
            }

            _iconGrid.Activate();

            foreach (var box in _iconGrid.Boxes)
            {
                MainGrid.Children.Add(box);
            }
        }

        private void CharacterSelected(object sender, TextEventArgs e)
        {
            Characters character = Character.GetCharacterFromName(e.Text);
            string narratorName = Character.GetNarratorNameFromCharacter(character);

            if (!String.IsNullOrEmpty(narratorName))
            {
                var player = new MediaPlayer();
                player.Open(new Uri(@"C:\Users\Takaji\Documents\GitHub\SmashFront\SmashFront\Resources\Narrator\Names\" + narratorName + ".wav"));
                player.Play();
            }

            this.Cursor = Cursors.None;
            _iconGrid.DisableControl();

            GameState state = new GameState();
            state.LaunchGame();
        }
    }
}
