﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RPG
{
    /// <summary>
    /// Interaction logic for CharacterEditingPage.xaml
    /// </summary>
    public partial class CharacterEditingPage : Page
    {
        private readonly Character EditingCharacter;
        public CharacterEditingPage(Character character)
        {
            InitializeComponent();
            EditingCharacter = character;

            characterNameTextBox.Text = EditingCharacter.Name;
            SetValues();

            SpeciesComboBox.ItemsSource = Enum.GetValues(typeof(Species));
            SpeciesComboBox.SelectedItem = EditingCharacter.Species;

            try
            {
                characterImage.Source = new BitmapImage(new Uri(EditingCharacter.ImageURL));
            }
            catch
            {
                SetRandomImage();
            }
        }

        private void SetValues()
        {
            StrengthLabel.Content = EditingCharacter.Strength.GetDescription();
            DexterityLabel.Content = EditingCharacter.Dexterity.GetDescription();
            VitalityLabel.Content = EditingCharacter.Vitality.GetDescription();
            MagicLabel.Content = EditingCharacter.Magic.GetDescription();
            SpeedLabel.Content = EditingCharacter.Speed.GetDescription();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveValues();

            if (!Main.Characters.Any(x => x.Equals(EditingCharacter))) Main.Characters.Add(EditingCharacter);
            CharacterListPage.SaveCharacters();

            var listPage = CurrentCharacterListPage.Instance;
            if (listPage != null) Main.LoadCharacters(listPage.CharacterGrid, Main.filePath, listPage.AddCharacterButton.Style, listPage.CharacterButton_Click, listPage.SelectCharacter);

            GoBack();
        }

        private void SaveValues()
        {
            EditingCharacter.Name = characterNameTextBox.Text.Replace("*", string.Empty);
            EditingCharacter.Species = (Species)SpeciesComboBox.SelectedItem;

            if (SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out SkillLevel? strength) && strength != null) EditingCharacter.Strength = (SkillLevel)strength;
            if (SkillLevelHelper.ParseEnum(DexterityLabel.Content.ToString() ?? string.Empty, out SkillLevel? dexterity) && dexterity != null) EditingCharacter.Dexterity = (SkillLevel)dexterity;
            if (SkillLevelHelper.ParseEnum(VitalityLabel.Content.ToString() ?? string.Empty, out SkillLevel? vitality) && vitality != null) EditingCharacter.Vitality = (SkillLevel)vitality;
            if (SkillLevelHelper.ParseEnum(MagicLabel.Content.ToString() ?? string.Empty, out SkillLevel? magic) && magic != null) EditingCharacter.Magic = (SkillLevel)magic;
            if (SkillLevelHelper.ParseEnum(SpeedLabel.Content.ToString() ?? string.Empty, out SkillLevel? speed) && speed != null) EditingCharacter.Speed = (SkillLevel)speed;

            EditingCharacter.ImageURL = characterImage.Source.ToString();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => GoBack();

        private static void GoBack() => CurrentMainWindow.Instance?.Frame.NavigationService.GoBack();

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
            var species = (Species)SpeciesComboBox.SelectedItem;
            switch (properties)
            {
                case Properties.Strength when SkillLevelHelper.ParseEnum(StrengthLabel.Content.ToString() ?? string.Empty, out var skillLevel) && skillLevel != null:
                    StrengthLabel.Content = GetValue((SkillLevel)skillLevel);
                    break;
                case Properties.Dexterity when SkillLevelHelper.ParseEnum(DexterityLabel.Content.ToString() ?? string.Empty, out var skillLevel) && skillLevel != null:
                    DexterityLabel.Content = GetValue((SkillLevel)skillLevel);
                    break;
                case Properties.Vitality when SkillLevelHelper.ParseEnum(VitalityLabel.Content.ToString() ?? string.Empty, out var skillLevel) && skillLevel != null:
                    VitalityLabel.Content = GetValue((SkillLevel)skillLevel);
                    break;
                case Properties.Magic when SkillLevelHelper.ParseEnum(MagicLabel.Content.ToString() ?? string.Empty, out var skillLevel) && skillLevel != null:
                    MagicLabel.Content = GetValue((SkillLevel)skillLevel);
                    break;
                case Properties.Speed when SkillLevelHelper.ParseEnum(SpeedLabel.Content.ToString() ?? string.Empty, out var skillLevel) && skillLevel != null:
                    SpeedLabel.Content = GetValue((SkillLevel)skillLevel);
                    break;
            }
            string GetValue(SkillLevel beforeValue) => ((SkillLevel)Math.Clamp((int)(increase ? ++beforeValue : --beforeValue), species == Species.Human ? (int)SkillLevel.Low : (int)SkillLevel.VeryLow, species == Species.Human ? (int)SkillLevel.High : (int)SkillLevel.VeryHigh)).GetDescription();
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

        private void SwapNameButton_Click(object sender, RoutedEventArgs e)
        {
            characterNameTextBox.Text = Main.RandomNames.RandomElement();
            characterNameTextBox.Focus();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            var species = (Species)SpeciesComboBox.SelectedItem;

            EditingCharacter.Species = species;
            EditingCharacter.SetAllValuesToDefault();
            SetValues();

            DisableButtons(species is Species.Human or Species.Enemy);
        }

        private void DisableButtons(bool enabled)
        {
            StrengthButtonMinus.IsEnabled = enabled;
            StrengthButtonPlus.IsEnabled = enabled;
            DexterityButtonMinus.IsEnabled = enabled;
            DexterityButtonPlus.IsEnabled = enabled;
            VitalityButtonMinus.IsEnabled = enabled;
            VitalityButtonPlus.IsEnabled = enabled;
            MagicButtonMinus.IsEnabled = enabled;
            MagicButtonPlus.IsEnabled = enabled;
            SpeedButtonMinus.IsEnabled = enabled;
            SpeedButtonPlus.IsEnabled = enabled;
        }

        private void RandomImageButton_Click(object sender, RoutedEventArgs e) => SetRandomImage();

        private void SetRandomImage()
        {
            string uriString = Main.ImageURLs.RandomElement() ?? string.Empty;

            try
            {
                // Do not touch - it took me 7 hours to get it working
                characterImage.Source = new BitmapImage(new Uri(uriString, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void DeleteCharacterAsync()
        {
            await Task.Delay(1);

            SaveValues();

            Main.Characters.Remove(EditingCharacter);

            CharacterListPage.SaveCharacters();

            var listPage = CurrentCharacterListPage.Instance;
            if (listPage != null) Main.LoadCharacters(listPage.CharacterGrid, Main.filePath, listPage.AddCharacterButton.Style, listPage.CharacterButton_Click, listPage.SelectCharacter);

            GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) => DeleteCharacterAsync();

        private void DeleteButton_MouseEnter(object sender, MouseEventArgs e) => DeleteLabel.Foreground = new SolidColorBrush(Colors.Black);

        private void DeleteButton_MouseLeave(object sender, MouseEventArgs e) => DeleteLabel.Foreground = new SolidColorBrush(Colors.Red);

        private void CancelButton_MouseEnter(object sender, MouseEventArgs e) => CancelLabel.Foreground = new SolidColorBrush(Colors.Black);

        private void CancelButton_MouseLeave(object sender, MouseEventArgs e) => CancelLabel.Foreground = new SolidColorBrush(Color.FromRgb(253, 187, 14));

        private void SaveButton_MouseEnter(object sender, MouseEventArgs e) => SaveLabel.Foreground = new SolidColorBrush(Colors.Black);

        private void SaveButton_MouseLeave(object sender, MouseEventArgs e) => SaveLabel.Foreground = new SolidColorBrush(Colors.LimeGreen);

        private void SpeciesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResetValues();
        }
    }
}
