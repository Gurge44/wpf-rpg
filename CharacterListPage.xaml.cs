using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            Main.LoadCharacters(CharacterGrid, Main.filePath, AddCharacterButton.Style, CharacterButton_Click, SelectCharacter);
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



        public void SelectCharacter(object sender, MouseButtonEventArgs e)
        {
            Window messageBox = new Window
            {
                Title = "Select Character",
                Width = 250,
                Height = 100,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow
            };

            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10)
            };

            Button selectButton = new Button
            {
                Content = "Select",
                Margin = new Thickness(5),
                Padding = new Thickness(10),
                Width = 75
            };
            selectButton.Click += (s, args) =>
            {
                MessageBox.Show("Character selected");
                messageBox.Close();
            };
            stackPanel.Children.Add(selectButton);

            Button cancelButton = new Button
            {
                Content = "Cancel",
                Margin = new Thickness(5),
                Padding = new Thickness(10),
                Width = 75
            };
            cancelButton.Click += (s, args) =>
            {
                messageBox.Close();
            };
            stackPanel.Children.Add(cancelButton);

            messageBox.Content = stackPanel;

            messageBox.ShowDialog();
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