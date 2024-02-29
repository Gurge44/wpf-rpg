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

                characterGrid.Margin = new Thickness(20, 0, 20, 30);

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


            characterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            characterGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

            Grid leftGrid = new Grid();
            leftGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < Main.Characters.Count; i++)
            {
                leftGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            }
            Image characterImage = new Image
            {
                Source = new BitmapImage(new Uri(character.ImageURL)),
                Stretch = Stretch.Uniform,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 30,
                Height = 30
            };
            leftGrid.Children.Add(characterImage);

            Grid rightGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };


            if (true)
            {
                ListBox propertyListBox = new ListBox
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    MaxHeight = 100
                };


                propertyListBox.Items.Add("Név: " + character.Name);

                propertyListBox.Items.Add("Species: " + character.Species.ToString());

                propertyListBox.Items.Add("Strenght: " + character.Strength.ToString());

                propertyListBox.Items.Add("Dexterity: " + character.Dexterity.ToString());

                propertyListBox.Items.Add("Vitality: " + character.Vitality.ToString());

                propertyListBox.Items.Add("Magic: " + character.Magic.ToString());

                propertyListBox.Items.Add("Speed: " + character.Speed.ToString());


                rightGrid.Children.Add(propertyListBox);
            }
            else if (false)
            {
                int row = 0;

                rightGrid.Children.Add(CreateTextBlock("Name: " + character.Name));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

                rightGrid.Children.Add(CreateTextBlock("Species: " + character.Species.ToString()));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

                rightGrid.Children.Add(CreateTextBlock("Erősség: " + character.Strength.ToString()));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

                rightGrid.Children.Add(CreateTextBlock("Ügyesség: " + character.Dexterity.ToString()));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

                rightGrid.Children.Add(CreateTextBlock("Vitalitás: " + character.Vitality.ToString()));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

                rightGrid.Children.Add(CreateTextBlock("Varázslat: " + character.Magic.ToString()));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

                rightGrid.Children.Add(CreateTextBlock("Sebesség: " + character.Speed.ToString()));
                rightGrid.Children[row].SetValue(Grid.RowProperty, row);
                row++;

            }

            leftGrid.SetValue(Grid.ColumnProperty, 0);
            rightGrid.SetValue(Grid.ColumnProperty, 1);

            characterGrid.Children.Add(leftGrid);
            characterGrid.Children.Add(rightGrid);

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

        private void main_grid_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
