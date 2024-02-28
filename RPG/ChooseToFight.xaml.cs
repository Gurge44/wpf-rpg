using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPG
{
    /// <summary>
    /// Interaction logic for ChooseToFight.xaml
    /// </summary>
    public partial class ChooseToFight : Window
    {
        private ScrollViewer scrollViewer;

        public ChooseToFight()
        {
            InitializeComponent();
            LoadCharacters(main_grid);
            SetBackgroundImage();
        }


        public void SetBackgroundImage()
        {
            ImageBrush backgroundImage = new ImageBrush();
            backgroundImage.ImageSource = new BitmapImage(new Uri("backg.jpg", UriKind.Relative));
            grid_g.Background = backgroundImage;
        }


         public void DisplayAllCharacters()
        {
            main_grid.Children.Clear();
            main_grid.ColumnDefinitions.Clear();

            int numColumns = 4;

            for (int i = 0; i < Main.Characters.Count; i++)
            {
                Character character = Main.Characters.ElementAt(i);

                Grid characterGrid = CreateCharacterGrid(character);

                main_grid.Children.Add(characterGrid);

                characterGrid.SetValue(Grid.ColumnProperty, i % numColumns);

                // Add spacing between characters
                characterGrid.Margin = new Thickness(20, 0, 20, 30);

                // Create a new column definition for each character
                main_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
        }

        public Grid CreateCharacterGrid(Character character)
        {
            Grid characterGrid = new Grid
            {
                Margin = new Thickness(20, 0, 20, 30),
                Visibility = Visibility.Visible
            };

            // Create two rows in the main grid
            characterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            characterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            // Upper grid for the character image
            Grid upperGrid = new Grid();
            upperGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            upperGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Image characterImage = new Image
            {
                Source = new BitmapImage(new Uri(character.ImageURL)),
                Stretch = Stretch.Uniform,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            upperGrid.Children.Add(characterImage);

            // Lower grid for character data
            Grid lowerGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            // Add a row for each property in the lower grid
            int numProperties = 0;
            if (!string.IsNullOrEmpty(character.Name))
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }
            if (character.Species != null)
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }
            if (character.Strength != null)
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }
            if (character.Dexterity != null)
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }
            if (character.Vitality != null)
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }
            if (character.Magic != null)
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }
            if (character.Speed != null)
            {
                lowerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                numProperties++;
            }

            if (numProperties > 4)
            {
                ListBox propertyListBox = new ListBox
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    MaxHeight = 100
                };

                if (!string.IsNullOrEmpty(character.Name))
                {
                    propertyListBox.Items.Add("Név: " + character.Name);
                }
                if (character.Species != null)
                {
                    propertyListBox.Items.Add("Faj: " + character.Species.ToString());
                }
                if (character.Strength != null)
                {
                    propertyListBox.Items.Add("Erősség: " + character.Strength.ToString());
                }
                if (character.Dexterity != null)
                {
                    propertyListBox.Items.Add("Ügyesség: " + character.Dexterity.ToString());
                }
                if (character.Vitality != null)
                {
                    propertyListBox.Items.Add("Vitalitás: " + character.Vitality.ToString());
                }
                if (character.Magic != null)
                {
                    propertyListBox.Items.Add("Varázslat: " + character.Magic.ToString());
                }
                if (character.Speed != null)
                {
                    propertyListBox.Items.Add("Sebesség: " + character.Speed.ToString());
                }

                lowerGrid.Children.Add(propertyListBox);
            }
            else
            {
                int row = 0;
                if (!string.IsNullOrEmpty(character.Name))
                {
                    lowerGrid.Children.Add(CreateTextBlock("Név: " + character.Name));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
                if (character.Species != null)
                {
                    lowerGrid.Children.Add(CreateTextBlock("Faj: " + character.Species.ToString()));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
                if (character.Strength != null)
                {
                    lowerGrid.Children.Add(CreateTextBlock("Erősség: " + character.Strength.ToString()));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
                if (character.Dexterity != null)
                {
                    lowerGrid.Children.Add(CreateTextBlock("Ügyesség: " + character.Dexterity.ToString()));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
                if (character.Vitality != null)
                {
                    lowerGrid.Children.Add(CreateTextBlock("Vitalitás: " + character.Vitality.ToString()));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
                if (character.Magic != null)
                {
                    lowerGrid.Children.Add(CreateTextBlock("Varázslat: " + character.Magic.ToString()));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
                if (character.Speed != null)
                {
                    lowerGrid.Children.Add(CreateTextBlock("Sebesség: " + character.Speed.ToString()));
                    lowerGrid.Children[row].SetValue(Grid.RowProperty, row);
                    row++;
                }
            }

            // Set grid placement
            upperGrid.SetValue(Grid.RowProperty, 0);
            lowerGrid.SetValue(Grid.RowProperty, 1);

            characterGrid.Children.Add(upperGrid);
            characterGrid.Children.Add(lowerGrid);

            return characterGrid;
        }

        public TextBlock CreateTextBlock(string text)
        {
            return new TextBlock
            {
                Text = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
        }

        public void LoadCharacters(Grid grid)
        {
            Main.Characters.Clear();
            grid.Children.RemoveRange(1, grid.Children.Count - 1);

            try
            {
                string[] lines = File.ReadAllLines(Main.filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split('*');

                    if (values.Length == 8)
                    {
                        Character character = new
                        (
                            name: values[0],
                            species: (Species)int.Parse(values[1]),
                            strength: (SkillLevel)int.Parse(values[2]),
                            dexterity: (SkillLevel)int.Parse(values[3]),
                            vitality: (SkillLevel)int.Parse(values[4]),
                            magic: (SkillLevel)int.Parse(values[5]),
                            speed: (SkillLevel)int.Parse(values[6]),
                            imageURL: values[7]
                        );
                        Main.Characters.Add(character);
                    }
                }
            }
            catch { }

            DisplayAllCharacters();
        }

        public void CharacterClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
