using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            CurrentCharacterListPage.Instance ??= new();

            Main.LoadCharacters(main_grid, Main.filePath, null, CharacterClick);
        }

        public void CreateMainGrid()
        {
            //                               if null => new
            CurrentCharacterListPage.Instance ??= new();

            Main.LoadCharacters(main_grid, Main.filePath, CurrentCharacterListPage.Instance.AddCharacterButton.Style, CharacterClick);

        }

        public void CharacterClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
