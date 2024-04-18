using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace RPG
{
    public static class Main
    {
        private static HashSet<Character> characters = [];
        private static HashSet<string> imageURLs = [];
        private static HashSet<string> randomNames = [];
        private static Random random = new();
        private static bool allowClose = false;
        public static Character SelectedCharacter;
        public static bool nextPage = false;

        public static HashSet<Character> Characters { get => characters; set => characters = value; }
        public static HashSet<string> ImageURLs { get => imageURLs; set => imageURLs = value; }
        public static HashSet<string> RandomNames { get => randomNames; set => randomNames = value; }
        public static Random Random { get => random; set => random = value; }

        public static HashSet<Character> Enemies => characters.Where(x => x.Species == Species.Enemy).ToHashSet();
        public static HashSet<Character> Team => characters.Where(x => x.Species != Species.Enemy).ToHashSet();

        public static bool AllowClose { get => allowClose; set => allowClose = value; }

        public const string filePath = "characters.txt";

        public static void LoadResources()
        {
            ImageURLs = [];
            RandomNames = [];

            for (int i = 1; i <= 14; i++)
            {
                // Do not touch - it took me 7 hours to get it working
                ImageURLs.Add($@"pack://application:,,,/RPG;component\Characters\char{i}.png");
            }

            RandomNames = [.. File.ReadAllLines("RandomNames.txt")];
        }

        public static void LoadCharacters()
        {
            Characters.Clear();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

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
                        Characters.Add(character);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Loads all characters from a specified file and puts them into a grid with the characters' images and names.
        /// </summary>
        /// <param name="grid">The grid to which the characters are outputted</param>
        /// <param name="filePath">The file's path to read the characters from</param>
        /// <param name="baseStyle">The base style of the buttons</param>
        /// <param name="handler">The method that runs when clicking on a character button in the grid</param>
        public static void LoadCharacters(Grid grid, string filePath, Style baseStyle, RoutedEventHandler handler, MouseButtonEventHandler rhandler)
        {
            grid.Children.RemoveRange(1, grid.Children.Count - 1);

            LoadCharacters();

            (int Column, int Row) = (0, 1);
            foreach (var character in Characters)
            {
                Button characterButton = new()
                {
                    Tag = character,
                    Style = baseStyle,
                    Padding = new Thickness(10, 10, 10, 10),
                    Margin = new Thickness(20, 0, 20, 30),
                    MinWidth = 50,
                    Visibility = Visibility.Visible,
                    FontSize = 10,
                };
                characterButton.Click += handler;
                characterButton.MouseRightButtonDown += rhandler;

                if (character.IsEnemy) characterButton.Foreground = new SolidColorBrush(Colors.Red);

                StackPanel buttonContent = new() { Orientation = Orientation.Vertical };

                Image characterImage = new() { Source = new BitmapImage(new Uri(character.ImageURL)), Width = 110, Margin = new Thickness(2, 2, 2, 2) };
                buttonContent.Children.Add(characterImage);

                TextBlock characterText = new() { Text = character.Name == string.Empty ? "Unnamed" : character.Name, HorizontalAlignment = HorizontalAlignment.Center };
                buttonContent.Children.Add(characterText);

                characterButton.Content = buttonContent;

                characterButton.SetValue(Grid.ColumnProperty, Column); Grid.SetColumn(characterButton, Column);
                characterButton.SetValue(Grid.RowProperty, Row); Grid.SetRow(characterButton, Row);

                Column++;
                if (Column > 6)
                {
                    Row++;
                    Column = 0;
                }
                if (Row == 3 && Column == 5) break;

                grid.Children.Add(characterButton);
            }
        }

        public static void SetMainWindowContents(Visibility visibility)
        {
            var mainwindow = CurrentMainWindow.Instance;
            if (mainwindow == null) return;

            mainwindow.TitleLabel.Visibility = visibility;
            mainwindow.NewGameButton.Visibility = visibility;
            mainwindow.LoadGameButton.Visibility = visibility;
            mainwindow.ManageCharactersButton.Visibility = visibility;
            mainwindow.QuitButton.Visibility = visibility;
        }

        public static void ShowFight(int level)
        {
            var x = new ChooseToFight(level)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            x.Show();
            AllowClose = true;
            CurrentMainWindow.Instance.Close();
        }
    }
}
