using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPG
{
    /// <summary>
    /// Interaction logic for ChooseToFight.xaml
    /// </summary>
    public partial class ChooseToFight : Window
    {
        public ChooseToFight()
        {
            InitializeComponent();
            Main.LoadCharacters();
            DisplayAllCharacters();
            SetBackgroundImage();
        }



        public void SetBackgroundImage()
        {
            ImageBrush backgroundImage = new()
            {
                ImageSource = new BitmapImage(new Uri("backg.jpg", UriKind.Relative))
            };
            grid_g.Background = backgroundImage;
        }


        public void CreateMainGrid()
        {
            main_grid.Children.Clear();
            main_grid.ColumnDefinitions.Clear();
            main_grid.RowDefinitions.Clear();

            Grid grid = new Grid();

          
        }

        public void DisplayAllCharacters()
        {
            Grid characterGrid = CreateCharacterGrid();
            left_grid.Children.Add(characterGrid);
        }

        public Grid CreateCharacterGrid()
        {
            Grid characterGrid = new Grid();
            Grid grid = new Grid();
            int db = 0;
            Grid lGrid = new();
            Grid rGrid = new();

            for (int i = 0; i < Main.Characters.Count; i++)
            {
                characterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            foreach (var c in Main.Characters)
            {
                lGrid.Children.Clear();
                rGrid.Children.Clear();
                lGrid.RowDefinitions.Clear();
                lGrid.ColumnDefinitions.Clear();
                rGrid.RowDefinitions.Clear();
                rGrid.ColumnDefinitions.Clear();

                grid.Children.Clear();
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();

                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });


                Image image = new()
                {
                    Source = new BitmapImage(new Uri(c.ImageURL, UriKind.Absolute)),
                    Stretch = Stretch.Fill
                };
                lGrid.Children.Add(image);

                TextBlock properties = new()
                {
                    Text = $"Name: {c.Name}\nSpecies: {c.Species}\nStrength: {c.Strength}\nDexterity: {c.Dexterity}\nVitality: {c.Vitality}\nMagic: {c.Magic}\nSpeed: {c.Speed}",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                };
                rGrid.Children.Add(properties);

                Grid.SetColumn(lGrid, 0);
                Grid.SetColumn(rGrid, 1);
                Grid.SetRow(lGrid, db);
                Grid.SetRow(rGrid, db);



                grid.Children.Add(lGrid);
                grid.Children.Add(rGrid);

                Grid.SetRow(grid, db);

                characterGrid.Children.Add(grid);
                db++;
            }
            return characterGrid;
        }


        public void CharacterClick(object sender, RoutedEventArgs e)
        {

        }

        private void main_grid_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
