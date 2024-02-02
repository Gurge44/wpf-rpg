using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RPG
{
    /// <summary>
    /// Interaction logic for CharacterListPage.xaml
    /// </summary>
    public partial class CharacterListPage : Page
    {
        private readonly MainWindow mainWindow;
        private const string filePath = "characters.txt";
        public CharacterListPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            LoadCharacters(true);
            CurrentCharacterListPage.Instance = this;
        }
        public void LoadCharacters(bool firstLoad)
        {
            if (firstLoad) Main.Characters = [];
            else characterButtonPanel.Children.RemoveRange(1, characterButtonPanel.Children.Count - 1);

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

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
            else
            {
                File.CreateText(filePath);
            }

            (int Column, int Row) = (1, 0);
            foreach (var character in Main.Characters)
            {
                Button characterButton = new()
                {
                    Content = character.Name == string.Empty ? "Unnamed" : character.Name,
                    Tag = character,
                    Style = AddCharacterButton.Style,
                    Padding = new Thickness(10, 10, 10, 10),
                    Margin = new Thickness(20, 0, 20, 5),
                    MinWidth = 50,
                    Visibility = Visibility.Visible,
                    FontSize = 20
                };
                characterButton.Click += CharacterButton_Click;

                characterButton.SetValue(Grid.ColumnProperty, Column); Grid.SetColumn(characterButton, Column);
                characterButton.SetValue(Grid.RowProperty, Row); Grid.SetRow(characterButton, Row);

                Column++;
                if (Column > 3)
                {
                    Row++;
                    Column = 0;
                    if (Row > 4) break;
                }

                characterButtonPanel.Children.Add(characterButton);
            }
        }

        public void SaveCharacters()
        {
            File.WriteAllLines(filePath, Main.Characters.Select(x => x.ToString() ?? string.Empty));
        }

        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button characterButton && characterButton.Tag is Character selectedCharacter)
            {
                mainWindow.NavigateToEditingPage(selectedCharacter);
            }
        }

        private void AddCharacter_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateToEditingPage(CharacterHelper.GetDefaultCharacter());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.TitleLabel.Visibility = Visibility.Visible;
            mainWindow.NewGameButton.Visibility = Visibility.Visible;
            mainWindow.LoadGameButton.Visibility = Visibility.Visible;
            mainWindow.ManageCharactersButton.Visibility = Visibility.Visible;

            characterButtonPanel.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            SaveCharacters();
        }
    }

    public static class CurrentCharacterListPage
    {
        public static CharacterListPage? Instance;
    }
}