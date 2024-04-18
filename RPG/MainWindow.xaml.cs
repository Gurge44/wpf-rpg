using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPG
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlayIntro();
            Main.AllowClose = false;
            Closing += (sender, e) => e.Cancel = !Main.AllowClose;
            CurrentMainWindow.Instance = this;
            Main.LoadResources();
        }
        public void NavigateToEditingPage(Character character)
        {
            Frame.NavigationService.Navigate(new CharacterEditingPage(character));
        }

        public void CopyIntroToBin()
        {
            // go back to the rpg folder
            string introPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "..", "..", "..", "intro.mp4");
            File.Copy(introPath, Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "intro.mp4"), true);
        }

        async void PlayIntro()
        {
            CopyIntroToBin();
            await Task.Delay(1);
            MainWindowGrid.Background = new SolidColorBrush(Colors.Black);
            Main.SetMainWindowContents(Visibility.Hidden);
            MediaElement introPlayer = new()
            {
                Source = new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "intro.mp4")),
                LoadedBehavior = MediaState.Play,
                UnloadedBehavior = MediaState.Close,
                Stretch = Stretch.Fill,
                Width = 1280,
                Height = 720
            };
            Grid.SetRowSpan(introPlayer, 6);
            Button skipButton = new()
            {
                Content = "Skip",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 10, 10),
                Background = new SolidColorBrush(Colors.Black),
                Foreground = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5),
                FontSize = 20
            };
            skipButton.Click += (sender, e) =>
            {
                MainWindowGrid.Children.Remove(introPlayer);
                MainWindowGrid.Children.Remove(skipButton);
                Main.SetMainWindowContents(Visibility.Visible);
                MainWindowGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/cs2.jpg")));
            };
            skipButton.MouseEnter += (sender, e) => skipButton.Foreground = new SolidColorBrush(Colors.Black);
            skipButton.MouseLeave += (sender, e) => skipButton.Foreground = new SolidColorBrush(Colors.White);
            Grid.SetRow(skipButton, 6);
            MainWindowGrid.Children.Add(introPlayer);
            MainWindowGrid.Children.Add(skipButton);
            introPlayer.MediaEnded += (sender, e) =>
            {
                MainWindowGrid.Children.Remove(introPlayer);
                MainWindowGrid.Children.Remove(skipButton);
                Main.SetMainWindowContents(Visibility.Visible);
                MainWindowGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/cs2.jpg")));
            };
        }

        private void ManageCharactersButton_Click(object sender, RoutedEventArgs e)
        {
            Main.SetMainWindowContents(Visibility.Hidden);

            CharacterListPage listPage;
            if (CurrentCharacterListPage.Instance != null)
            {
                listPage = CurrentCharacterListPage.Instance;

                listPage.AddCharacterButton.Visibility = Visibility.Visible;
                listPage.CharacterGrid.Visibility = Visibility.Visible;
                listPage.characterButtonPanel.Visibility = Visibility.Visible;
                listPage.BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                listPage = new CharacterListPage();
            }
            Frame.NavigationService.Navigate(listPage);
        }

        private void QuitButton_MouseEnter(object sender, MouseEventArgs e) => QuitLabel.Foreground = new SolidColorBrush(Colors.Black);

        private void QuitButton_MouseLeave(object sender, MouseEventArgs e) => QuitLabel.Foreground = new SolidColorBrush(Colors.White);

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Main.AllowClose = true;
            Close();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Main.nextPage)
            {
                ManageCharactersButton_Click(sender, e);
                return;
            }
            Main.SetMainWindowContents(Visibility.Hidden);
            Frame.NavigationService.Navigate(new Room());
        }

        public void LoadGameButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public static class CurrentMainWindow
    {
        private static MainWindow? instance;

        public static MainWindow? Instance { get => instance; set => instance = value; }
    }
}