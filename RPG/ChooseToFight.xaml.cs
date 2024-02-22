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
            CreateMainGrid();
        }

        public void CreateMainGrid()
        {
            //                               if null => new
            CurrentCharacterListPage.Instance ??= new();
            /*
            foreach (var x in Main.Characters)
            {
                Grid grid = new Grid
                {
                    Children =
                    {
                        Label label = new Label
                        {
                            Content = x.Name
                        };
                    }
                };
            }*/

            Main.LoadCharacters(main_grid, Main.filePath, null, CharacterClick);

        }

        public void CharacterClick(object sender, RoutedEventArgs e)
        {

        }

    }
}
