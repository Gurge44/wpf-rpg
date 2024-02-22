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
        public CharacterListPage()
        {
            InitializeComponent();
            Main.LoadCharacters(CharacterGrid, Main.filePath, AddCharacterButton.Style, CharacterButton_Click);
            CurrentCharacterListPage.Instance = this;
        }
        
        public static void SaveCharacters() => File.WriteAllLines(Main.filePath, Main.Characters.Select(x => x.ToString() ?? string.Empty));

        public void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button characterButton && characterButton.Tag is Character selectedCharacter)
            {
                CurrentMainWindow.Instance?.NavigateToEditingPage(selectedCharacter);
            }
        }

        private void AddCharacter_Click(object sender, RoutedEventArgs e) => CurrentMainWindow.Instance?.NavigateToEditingPage(CharacterHelper.GetDefaultCharacter());

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Main.SetMainWindowContents(Visibility.Visible);

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