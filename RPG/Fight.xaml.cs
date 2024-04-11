using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace RPG
{
    /// <summary>
    /// Interaction logic for Fight.xaml
    /// </summary>
    public partial class Fight : Page
    {
        private Tetris tetris;

        public Fight()
        {
            InitializeComponent();
            MainTetris.FightInstance(this);
            MainFight.Fightinstance(this);
            SetHealths();
            MainFight.MainGrid = main_grid;
            if (Main.SelectedCharacter != null)
            {
                CreateEnemyGrid();
                CreateCharacterGrid();
                tetris = new Tetris();
                tetris.InitializeMain(1);
                InitializeTimer();
            }
            else
            {
                MessageBox.Show("Please select a character to fight with");
            }

        }

        public void SetHealths()
        {
            MainFight.characterHealth = 1000;
            MainFight.enemyHealth = 200;
            MainFight.Max_C = MainFight.characterHealth;
            MainFight.Max_E = MainFight.enemyHealth;
        }

        public void CreateEnemyGrid()
        {
            if (Main.Enemies == null || Main.Enemies.Count == 0)
                return;
            enemy_picture_grid.Children.Clear();
            Image enemy = new Image();
            Character x = Main.Enemies.RandomElement();
            if (x != null)
                enemy.Source = new BitmapImage(new Uri(x.ImageURL, UriKind.Absolute));


            Grid.SetRow(enemy, 0);
            Grid.SetColumn(enemy, 0);
            enemy_picture_grid.Children.Add(enemy);

            Grid grid_name = new Grid();
            Label name = new Label();
            name.Content = x.Name;
            name.FontSize = 20;
            name.Foreground = Brushes.White;
            name.FontWeight = FontWeights.Bold;
            name.HorizontalContentAlignment = HorizontalAlignment.Left;
            name.VerticalContentAlignment = VerticalAlignment.Bottom;
            grid_name.Children.Add(name);
            Grid.SetRow(grid_name, 0);
            Grid.SetRow(MainFight.enemyHprogressBar, 1);
            enemy_name_grid.Children.Add(grid_name);
            enemy_name_grid.Children.Add(MainFight.enemyHprogressBar);
            EnemyProgressBar(MainFight.enemyHprogressBar);
        }

        public void CreateCharacterGrid()
        {
            if (Main.Team == null || Main.Team.Count == 0)
                return;
            character_picture_grid.Children.Clear();
            Image character = new Image();
            Character x = Main.SelectedCharacter;
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
            CharacterProgressBar(progressBar);

        }

        public void CharacterProgressBar(ProgressBar characterHealth)
        {
            characterHealth.Maximum = MainFight.Max_C;
            characterHealth.Minimum = 0;
            characterHealth.Value = MainFight.characterHealth;
            characterHealth.Foreground = Brushes.Green;
            characterHealth.Background = Brushes.White;
            characterHealth.Orientation = Orientation.Horizontal;
            characterHealth.Margin = new Thickness(0, 0, 0, 0);
        }

        public void EnemyProgressBar(ProgressBar enemyHealth)
        {
            enemyHealth.Maximum = MainFight.Max_E;
            enemyHealth.Minimum = 0;
            enemyHealth.Value = MainFight.enemyHealth;
            enemyHealth.Foreground = Brushes.Red;
            enemyHealth.Background = Brushes.White;
            enemyHealth.Orientation = Orientation.Horizontal;
            enemyHealth.Margin = new Thickness(0, 0, 0, 0);
        }

        public void InitializeTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMicroseconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (MainFight.time == 0)
            {
                MainFight.EndCurrentAttack();
                MainFight.time = 10000;
                tetris.CreateMainGrid();
            }
            else
            {
                MainFight.time--;
            }
            timer_pb.Value = MainFight.time;
        }

        public void TimerProgressBar()
        {
            ProgressBar timer = timer_pb;
            timer.Maximum = 10000;
            timer.Minimum = 0;
            timer.Value = MainFight.time;
            timer.Foreground = Brushes.Gray;
            timer.Background = Brushes.White;
            timer.Orientation = Orientation.Horizontal;
            timer.Margin = new Thickness(0, 0, 0, 0);
        }
    }
}
