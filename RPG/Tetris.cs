using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RPG
{
    public class Tetris
    {
        public void InitializeMain(int level)
        {
            MainTetris.Width = 20;
            MainTetris.Height = 10;
            MainTetris.Score = 0;
            MainTetris.Lines = 0;
            MainTetris.Level = level;
            MainTetris.Blocks = new Block[MainTetris.Width, MainTetris.Height];
            MainTetris.CurrentPiece = new Piece();
            MainTetris.NextPiece = new Piece();
            MainTetris.IsNewPiece = true;
            MainFight.Instance.TimerProgressBar();
            CreateMainGrid();
        }
        public void CreateMainGrid()
        {
            MainFight.MainGrid.Children.Clear();
            MainFight.MainGrid.ColumnDefinitions.Clear();
            MainFight.MainGrid.RowDefinitions.Clear();
            for (int i = 0; i < MainTetris.Width; i++)
            {
                MainFight.MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < MainTetris.Height; i++)
            {
                MainFight.MainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int x = 0; x < MainTetris.Width; x++)
            {
                for (int y = 0; y < MainTetris.Height; y++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = Brushes.White;
                    Grid.SetColumn(rectangle, x);
                    Grid.SetRow(rectangle, y);
                    MainFight.MainGrid.Children.Add(rectangle);
                    FillMatrix(x, y, rectangle);
                }
            }
        }

        public void FillMatrix(int x, int y, Rectangle rectangle)
        {
            MainTetris.Blocks[x, y] = new Block();
            MainTetris.Blocks[x, y].Type = BlockType.Piece;
            MainTetris.Blocks[x, y].X = x;
            MainTetris.Blocks[x, y].Y = y;
            MainTetris.Blocks[x, y].Rectangle = rectangle;
            MainTetris.DrawBlock(x, y);
        }

        public void Start()
        {
            if (MainTetris.IsNewGame)
            {
                MainTetris.IsNewGame = false;
                MainTetris.IsGameOver = false;
                MainTetris.IsPaused = false;
                MainTetris.IsStarted = true;
                MainTetris.Score = 0;
                MainTetris.Lines = 0;
                MainTetris.Level = 1;
                MainTetris.LinesCleared = 0;
                MainTetris.CurrentPiece = new Piece();
                MainTetris.NextPiece = new Piece();
                MainTetris.GameOver = false;
                MainTetris.Paused = false;
                MainTetris.Started = true;
                MainTetris.IsNewPiece = true;
            }
        }

        public void Pause()
        {
            if (MainTetris.IsStarted && !MainTetris.IsGameOver)
            {
                MainTetris.IsPaused = !MainTetris.IsPaused;
                MainTetris.Paused = !MainTetris.Paused;
            }
        }

        public void End()
        {
            MainTetris.IsGameOver = true;
            MainTetris.IsPaused = false;
            MainTetris.IsStarted = false;
            MainTetris.GameOver = true;
        }

        public void Restart()
        {
            MainTetris.IsNewGame = true;
            MainTetris.IsGameOver = false;
            MainTetris.IsPaused = false;
            MainTetris.IsStarted = false;
            MainTetris.GameOver = false;
            MainTetris.Paused = false;
            MainTetris.Started = false;
            MainTetris.IsNewPiece = false;
        }

        public void Update()
        {
            if (MainTetris.IsStarted && !MainTetris.IsPaused && !MainTetris.IsGameOver)
            {
                if (MainTetris.IsNewPiece)
                {
                    MainTetris.CurrentPiece = MainTetris.NextPiece;
                    MainTetris.NextPiece = new Piece();
                    MainTetris.IsNewPiece = false;
                }
            }
        }

        public void MoveLeft()
        {
            if (MainTetris.IsStarted && !MainTetris.IsPaused && !MainTetris.IsGameOver)
            {
                if (MainTetris.CurrentPiece != null)
                {
                    MainTetris.CurrentPiece.X--;
                    if (Collision())
                    {
                        MainTetris.CurrentPiece.X++;
                    }
                }
            }
        }

        public void MoveRight()
        {
            if (MainTetris.IsStarted && !MainTetris.IsPaused && !MainTetris.IsGameOver)
            {
                if (MainTetris.CurrentPiece != null)
                {
                    MainTetris.CurrentPiece.X++;
                    if (Collision())
                    {
                        MainTetris.CurrentPiece.X--;
                    }
                }
            }
        }

        public void MoveDown()
        {
            if (MainTetris.IsStarted && !MainTetris.IsPaused && !MainTetris.IsGameOver)
            {
                if (MainTetris.CurrentPiece != null)
                {
                    MainTetris.CurrentPiece.Y++;
                    if (Collision())
                    {
                        MainTetris.CurrentPiece.Y--;
                        LockPiece();
                    }
                }
            }
        }

        public void Rotate()
        {
            if (MainTetris.IsStarted && !MainTetris.IsPaused && !MainTetris.IsGameOver)
            {
                if (MainTetris.CurrentPiece != null)
                {
                    MainTetris.CurrentPiece.Rotation++;
                    if (Collision())
                    {
                        MainTetris.CurrentPiece.Rotation--;
                    }
                }
            }
        }

        public void LockPiece()
        {
            if (MainTetris.CurrentPiece != null)
            {
                for (int i = 0; i < MainTetris.CurrentPiece.Blocks.Count; i++)
                {
                    Block block = MainTetris.CurrentPiece.Blocks[i];
                    MainTetris.Blocks[block.X, block.Y] = block;
                }
                CheckLines();
                MainTetris.IsNewPiece = true;
            }
        }

        public void CheckLines()
        {
            for (int y = MainTetris.Height - 1; y >= 0; y--)
            {
                bool full = true;
                for (int x = 0; x < MainTetris.Width; x++)
                {
                    if (MainTetris.Blocks[x, y] == null)
                    {
                        full = false;
                        break;
                    }
                }
                if (full)
                {
                    MainTetris.Lines++;
                    MainTetris.LinesCleared++;
                    MainTetris.Score += 100;
                    for (int i = y; i > 0; i--)
                    {
                        for (int x = 0; x < MainTetris.Width; x++)
                        {
                            MainTetris.Blocks[x, i] = MainTetris.Blocks[x, i - 1];
                        }
                    }
                    y++;
                }
            }
        }
        public static bool Collision()
        {
            if (MainTetris.CurrentPiece != null)
            {
                for (int i = 0; i < MainTetris.CurrentPiece.Blocks.Count; i++)
                {
                    Block block = MainTetris.CurrentPiece.Blocks[i];
                    if (block.X < 0 || block.X >= MainTetris.Width || block.Y >= MainTetris.Height || MainTetris.Blocks[block.X, block.Y] != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void Move(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    MoveLeft();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Up:
                    Rotate();
                    break;
            }
        }
    }
}
