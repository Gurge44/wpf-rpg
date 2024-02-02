using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //NavigateToEditingPage(CharacterHelper.GetDefaultCharacter());
            Main.LoadResources();
        }
        public void NavigateToEditingPage(Character character)
        {
            Frame.NavigationService.Navigate(new CharacterEditingPage(character, this));
        }

        private void ManageCharactersButton_Click(object sender, RoutedEventArgs e)
        {
            TitleLabel.Visibility = Visibility.Hidden;
            NewGameButton.Visibility = Visibility.Hidden;
            LoadGameButton.Visibility = Visibility.Hidden;
            ManageCharactersButton.Visibility = Visibility.Hidden;

            Frame.NavigationService.Navigate(new CharacterListPage(this));
        }
    }
}