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
            MainFight.Fightinstance(this);
            CreateEnemyGrid();
            CreateCharacterGrid();
            InitializeMain();
            InitializeTimer();
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
            if (Main.Team == null || Main.Team.Count == 0)
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

        public void InitializeMain()
        {
            MainFight.time = 10;
            MainFight.Width = 20;
            MainFight.Height = 20;
            MainFight.Blocks = new Block[MainFight.Width, MainFight.Height];
            CreateMainGrid();
            TimerProgressBar();
        }

        public void InitializeTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            if (MainFight.time == 0)
            {
                MainFight.EndCurrentAttack();
                MainFight.time = 10;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (MainFight.time == 0)
            {
                MainFight.EndCurrentAttack();
                MainFight.time = 10;
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
            timer.Maximum = 10;
            timer.Minimum = 0;
            timer.Value = MainFight.time;
            timer.Foreground = Brushes.Gray;
            timer.Background = Brushes.White;
            timer.Orientation = Orientation.Horizontal;
            timer.Margin = new Thickness(0, 0, 0, 0);
        }


        public void CreateMainGrid()
        {
            for (int i = 0; i < MainFight.Width; i++)
            {
                main_grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < MainFight.Height; i++)
            {
                main_grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < MainFight.Width; i++)
            {
                for (int j = 0; j < MainFight.Height; j++)
                {
                    Button button = new Button();
                    button.Click += Button_Click;
                    button.Name = "Button_" + i + "_" + j;
                    button.Style = (Style)FindResource("BoosterGame");
                    button.FontSize = 20;
                    Grid.SetRow(button, j);
                    Grid.SetColumn(button, i);
                    main_grid.Children.Add(button);
                    FillMatrix(i, j, button);
                }
            }
        }

        public void FillMatrix(int x, int y, Button button)
        {
            MainFight.Blocks[x, y] = new Block();
            MainFight.Blocks[x, y].X = x;
            MainFight.Blocks[x, y].Y = y;
            MainFight.Blocks[x, y].Type = MainFight.SetRandomType(x, y);
            MainFight.Blocks[x, y].Button = button;
            MainFight.DrawBlock(x, y);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string[] coordinates = button.Name.Split('_');
            int x = int.Parse(coordinates[1]);
            int y = int.Parse(coordinates[2]);
            MainFight.HandleType(x, y);
            MainFight.DrawBlock(x, y);
        }

        public void SetRandomColor()
        {
            Random random = new Random();

        }

        public void GameOver()
        {
            //Process.Start("suffer.exe");
        }
    }
}
