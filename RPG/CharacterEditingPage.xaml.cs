using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for CharacterEditingPage.xaml
    /// </summary>
    public partial class CharacterEditingPage : Page
    {
        private readonly Character editingCharacter;
        private readonly MainWindow mainWindow;
        public CharacterEditingPage(Character character, MainWindow mainWindow)
        {
            InitializeComponent();
            this.editingCharacter = character;
            this.mainWindow = mainWindow;

            characterNameTextBox.Text = editingCharacter.Name;
            property1Label.Content = editingCharacter.Strength.ToString();
            // ... Set values for other properties
        }
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle + button click and update UI
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle - button click and update UI
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            editingCharacter.Name = characterNameTextBox.Text;
            editingCharacter.Strength = (SkillLevel)int.Parse(property1Label.Content.ToString() ?? "0");
            // ... Update other properties

            mainWindow.Frame.NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frame.NavigationService.GoBack();
        }
    }
}
