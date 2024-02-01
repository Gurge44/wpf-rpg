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
            editingCharacter = character;
            this.mainWindow = mainWindow;

            characterNameTextBox.Text = editingCharacter.Name;

            StrengthLabel.Content = editingCharacter.Strength.GetDescription();
            DexterityLabel.Content = editingCharacter.Dexterity.GetDescription();
            VitalityLabel.Content = editingCharacter.Vitality.GetDescription();
            MagicLabel.Content = editingCharacter.Magic.GetDescription();
            SpeedLabel.Content = editingCharacter.Speed.GetDescription();

            SpeciesComboBox.ItemsSource = Enum.GetValues(typeof(Species));
            SpeciesComboBox.SelectedItem = editingCharacter.Species;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            editingCharacter.Name = characterNameTextBox.Text;
            editingCharacter.Species = (Species)SpeciesComboBox.SelectedItem;

            if (SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out SkillLevel? skillLevel1) && skillLevel1 != null) editingCharacter.Strength = (SkillLevel)skillLevel1;
            if (SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out SkillLevel? skillLevel2) && skillLevel2 != null) editingCharacter.Dexterity = (SkillLevel)skillLevel2;
            if (SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out SkillLevel? skillLevel3) && skillLevel3 != null) editingCharacter.Vitality = (SkillLevel)skillLevel3;
            if (SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out SkillLevel? skillLevel4) && skillLevel4 != null) editingCharacter.Magic = (SkillLevel)skillLevel4;
            if (SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out SkillLevel? skillLevel5) && skillLevel5 != null) editingCharacter.Speed = (SkillLevel)skillLevel5;

            Main.Characters.Add(editingCharacter);
            mainWindow.Frame.NavigationService.GoBack();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frame.NavigationService.GoBack();
        }

        enum Properties
        {
            Strength,
            Dexterity,
            Vitality,
            Magic,
            Speed
        }

        private void UpdateValue(bool increase, Properties properties)
        {
            switch (properties)
            {
                case Properties.Strength:
                    StrengthLabel.Content = GetValue(editingCharacter.Strength);
                    break;
                case Properties.Dexterity:
                    DexterityLabel.Content = GetValue(editingCharacter.Dexterity);
                    break;
                case Properties.Vitality:
                    VitalityLabel.Content = GetValue(editingCharacter.Vitality);
                    break;
                case Properties.Magic:
                    MagicLabel.Content = GetValue(editingCharacter.Magic);
                    break;
                case Properties.Speed:
                    SpeedLabel.Content = GetValue(editingCharacter.Speed);
                    break;
            }
            string GetValue(SkillLevel beforeValue) => ((SkillLevel)Math.Clamp((int)(increase ? ++beforeValue : --beforeValue), (int)SkillLevel.VeryLow, (int)SkillLevel.VeryHigh)).GetDescription();
        }

        private void StrengthButtonPlus_Click(object sender, RoutedEventArgs e) => UpdateValue(true, Properties.Strength);
        private void StrengthButtonMinus_Click(object sender, RoutedEventArgs e) => UpdateValue(false, Properties.Strength);
        private void DexterityButtonPlus_Click(object sender, RoutedEventArgs e) => UpdateValue(true, Properties.Dexterity);
        private void DexterityButtonMinus_Click(object sender, RoutedEventArgs e) => UpdateValue(false, Properties.Dexterity);
        private void VitalityButtonPlus_Click(object sender, RoutedEventArgs e) => UpdateValue(true, Properties.Vitality);
        private void VitalityButtonMinus_Click(object sender, RoutedEventArgs e) => UpdateValue(false, Properties.Vitality);
        private void MagicButtonPlus_Click(object sender, RoutedEventArgs e) => UpdateValue(true, Properties.Magic);
        private void MagicButtonMinus_Click(object sender, RoutedEventArgs e) => UpdateValue(false, Properties.Magic);
        private void SpeedButtonPlus_Click(object sender, RoutedEventArgs e) => UpdateValue(true, Properties.Speed);
        private void SpeedButtonMinus_Click(object sender, RoutedEventArgs e) => UpdateValue(false, Properties.Speed);
    }
}
