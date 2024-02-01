using System.Windows;
using System.Windows.Controls;

namespace RPG
{
    /// <summary>
    /// Interaction logic for CharacterListPage.xaml
    /// </summary>
    public partial class CharacterListPage : Page
    {
        private MainWindow mainWindow;
        public CharacterListPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            LoadCharacters();
        }
        private void LoadCharacters()
        {
            // Your existing code to load characters and create buttons
            // ...

            foreach (var character in characters)
            {
                Button characterButton = new Button
                {
                    Content = character.Name,
                    Tag = character,
                    Click += CharacterButton_Click
                };

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
            mainWindow.NavigateToEditingPage(new Character());
        }
    }
}