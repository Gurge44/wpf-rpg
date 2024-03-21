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
    /// Interaction logic for Fight.xaml
    /// </summary>
    public partial class Fight : Page
    {
        public interface IFight
        {
            int Health { get; set; }
            int Damage { get; set; }
            int Level { get; set; }
            int Experience { get; set; }
        }
        public Fight()
        {
            InitializeComponent();
            CreateEnemyGrid();
            CreateCharacterGrid();
        }

        public void CreateEnemyGrid()
        {
            if (Main.Enemies == null)
                return;
            enemy_picture_grid.Children.Clear();
            Image enemy = new Image();
            Character x = Main.Enemies.RandomElement();
            enemy.Source = new BitmapImage(new Uri(x.ImageURL, UriKind.Absolute));

            Grid.SetRow(enemy, 0);
            Grid.SetColumn(enemy, 0);
            enemy_picture_grid.Children.Add(enemy);

            Grid grid_name = new Grid();
            ProgressBar progressBar = new ProgressBar();
            Label name = new Label();
            name.Content = x.Name;
            name.FontSize = 20;
            name.Foreground = Brushes.White;
            name.FontWeight = FontWeights.Bold;
            name.HorizontalContentAlignment = HorizontalAlignment.Left;
            name.VerticalContentAlignment = VerticalAlignment.Bottom;

            grid_name.Children.Add(name);
            Grid.SetRow(grid_name, 0);
            Grid.SetRow(progressBar, 1);
            enemy_name_grid.Children.Add(grid_name);
            enemy_name_grid.Children.Add(progressBar);




        }

        public void CreateCharacterGrid()
        {
            if (Main.Team == null)
                return;
            character_picture_grid.Children.Clear();
            Image character = new Image();
            Character x = Main.Team.RandomElement();
            character.Source = new BitmapImage(new Uri(x.ImageURL, UriKind.Absolute));

            Grid.SetRow(character, 0);
            Grid.SetColumn(character, 0);
            character_picture_grid.Children.Add(character);

            Grid grid_name = new Grid();
            ProgressBar progressBar = new ProgressBar();
            Label name = new Label();
            name.Content = x.Name;
            name.FontSize = 20;
            name.Foreground = Brushes.White;
            name.FontWeight = FontWeights.Bold;
            name.HorizontalContentAlignment = HorizontalAlignment.Right;
            name.VerticalContentAlignment = VerticalAlignment.Bottom;

            grid_name.Children.Add(name);
            Grid.SetRow(grid_name, 0);
            Grid.SetRow(progressBar, 1);
            character_name_grid.Children.Add(grid_name);
            character_name_grid.Children.Add(progressBar);
        }


    }
}
