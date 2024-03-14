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

            ScrollViewer scrollViewer = new ScrollViewer();
            characterGrid.Children.Add(scrollViewer);
            Grid.SetRow(scrollViewer, 0);

            Grid grid = new Grid();
            scrollViewer.Content = grid;


            Grid characterRow = CreateCharacterRow();
            grid.Children.Add(characterRow);

            return characterGrid;
        }

        public Grid CreateCharacterRow()
        {
            Grid characterGrid = new Grid();
            int db = 0;

            foreach (var character in Main.Characters)
            {
                characterGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

                Grid grid = new Grid
                {
                    Margin = new Thickness(0, 0, 0, 10)
                };
                Grid lGrid = new Grid();
                Grid rGrid = new Grid();

                lGrid.Children.Clear();
                rGrid.Children.Clear();
                lGrid.RowDefinitions.Clear();
                lGrid.ColumnDefinitions.Clear();
                rGrid.RowDefinitions.Clear();
                rGrid.ColumnDefinitions.Clear();

                grid.Children.Clear();
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();

                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(character.ImageURL, UriKind.Absolute)),
                    Stretch = Stretch.Fill,                    
                };
                
                lGrid.Children.Add(image);

                TextBlock properties = new TextBlock
                {
                    Text = $"Name: {character.Name}\nSpecies: {character.Species}\nStrength: {character.Strength}\nDexterity: {character.Dexterity}\nVitality: {character.Vitality}\nMagic: {character.Magic}\nSpeed: {character.Speed}",
                    FontSize = 20,
                    Foreground = new SolidColorBrush(Colors.White),
                };
                rGrid.Children.Add(properties);

                Grid.SetColumn(lGrid, 0);
                Grid.SetColumn(rGrid, 1);
                Grid.SetRow(lGrid, 0);
                Grid.SetRow(rGrid, 0);

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
