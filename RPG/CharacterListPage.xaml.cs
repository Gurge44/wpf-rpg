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
            LoadCharacters();
            CurrentCharacterListPage.Instance = this;
        }
        private void LoadCharacters()
        {
            Main.Characters = [];

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

            foreach (var character in Main.Characters)
            {
                Button characterButton = new()
                {
                    Content = character.Name,
                    Tag = character
                };
                characterButton.Click += CharacterButton_Click;

                characterButtonPanel.Children.Add(characterButton);
            }
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
    }

    public static class CurrentCharacterListPage
    {
        public static CharacterListPage? Instance;
    }
}