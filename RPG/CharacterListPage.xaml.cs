using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPG
{
    /// <summary>
    /// Interaction logic for CharacterListPage.xaml
    /// </summary>
    public partial class CharacterListPage : Page
    {
        private const string filePath = "characters.txt";
        public CharacterListPage()
        {
            InitializeComponent();
            LoadCharacters();
            CurrentCharacterListPage.Instance = this;
        }
        public void LoadCharacters()
        {
            Main.Characters.Clear();
            CharacterGrid.Children.RemoveRange(1, CharacterGrid.Children.Count - 1);

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
                        Main.Characters.Add(character);
                    }
                }
            }
            catch { }

            (int Column, int Row) = (0, 1);
            foreach (var character in Main.Characters)
            {
                Button characterButton = new()
                {
                    Tag = character,
                    Style = AddCharacterButton.Style,
                    Padding = new Thickness(10, 10, 10, 10),
                    Margin = new Thickness(20, 0, 20, 30),
                    MinWidth = 50,
                    Visibility = Visibility.Visible,
                    FontSize = 10,
                };
                characterButton.Click += CharacterButton_Click;

                if (character.IsEnemy) characterButton.Foreground = new SolidColorBrush(Colors.Red);


                StackPanel buttonContent = new() { Orientation = Orientation.Vertical };


                // Add the image
                Image characterImage = new() { Source = new BitmapImage(new Uri(character.ImageURL)), Width = 110, Margin = new Thickness(2, 2, 2, 2) };
                buttonContent.Children.Add(characterImage);

                // Add the text content
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

                CharacterGrid.Children.Add(characterButton);
            }
        }

        public static void SaveCharacters() => File.WriteAllLines(filePath, Main.Characters.Select(x => x.ToString() ?? string.Empty));

        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button characterButton && characterButton.Tag is Character selectedCharacter)
            {
                CurrentMainWindow.Instance?.NavigateToEditingPage(selectedCharacter);
            }
        }

        private void AddCharacter_Click(object sender, RoutedEventArgs e) => CurrentMainWindow.Instance?.NavigateToEditingPage(CharacterHelper.GetDefaultCharacter());

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMainWindow.Instance != null)
            {
                CurrentMainWindow.Instance.TitleLabel.Visibility = Visibility.Visible;
                CurrentMainWindow.Instance.NewGameButton.Visibility = Visibility.Visible;
                CurrentMainWindow.Instance.LoadGameButton.Visibility = Visibility.Visible;
                CurrentMainWindow.Instance.ManageCharactersButton.Visibility = Visibility.Visible;
                CurrentMainWindow.Instance.QuitButton.Visibility = Visibility.Visible;
            }

            characterButtonPanel.Visibility = Visibility.Hidden;
            CharacterGrid.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Hidden;

            SaveCharacters();
        }

        private void BackButton_MouseEnter(object sender, MouseEventArgs e) => BackLabel.Foreground = new SolidColorBrush(Colors.Black);

        private void BackButton_MouseLeave(object sender, MouseEventArgs e) => BackLabel.Foreground = new SolidColorBrush(Color.FromRgb(105, 130, 239));
    }

    public static class CurrentCharacterListPage
    {
        private static CharacterListPage? instance;

        public static CharacterListPage? Instance { get => instance; set => instance = value; }
    }
}