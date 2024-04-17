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
using System.Windows.Threading;

namespace RPG
{
    public class Tetris
    {
        public Tetris(int level)
        {
            InitializeGame(level);
            CreateMainGrid();
            CreatePiece();
            InitializeTimer();
        }
        public void InitializeGame(int level)
        {
            MainTetris.Random = new Random();
            MainTetris.Width = 10;
            MainTetris.Height = 20;
            MainTetris.Blocks = new Block[MainTetris.Width, MainTetris.Height];
            MainTetris.PositionX = MainTetris.Width / 2;
            MainTetris.PositionY = 0;
        }

        public void InitializeTimer()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            MoveToDirection(Direction.Down);
        }

        public void CreateMainGrid()
        {
            MainFight.Instance.main_grid.ColumnDefinitions.Clear();
            MainFight.Instance.main_grid.RowDefinitions.Clear();
            MainFight.Instance.main_grid.Children.Clear();
            for (int i = 0; i < MainTetris.Width; i++)
            {
                MainFight.Instance.main_grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < MainTetris.Height; i++)
            {
                MainFight.Instance.main_grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < MainTetris.Width; i++)
            {
                for (int j = 0; j < MainTetris.Height; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Stroke = Brushes.Black;
                    rectangle.StrokeThickness = 1;
                    Grid.SetColumn(rectangle, i);
                    Grid.SetRow(rectangle, j);
                    MainFight.Instance.main_grid.Children.Add(rectangle);
                    FillMatrix(i, j, rectangle);
                }
            }

        }

        public void FillMatrix(int x, int y, Rectangle rectangle)
        {
            MainTetris.Blocks[x, y] = new Block();
            MainTetris.Blocks[x, y].Type = BlockType.Empty;
            MainTetris.Blocks[x, y].X = x;
            MainTetris.Blocks[x, y].Y = y;
            MainTetris.Blocks[x, y].Rectangle = rectangle;
            MainTetris.Blocks[x, y].IsLocked = false;
            MainTetris.DrawBlock(x, y);
        }

        public void CreatePiece()
        {
            MainTetris.PositionX = MainTetris.Width / 2;
            MainTetris.PositionY = 0;
            MainTetris.CurrentBlock = new Block();
            MainTetris.CurrentBlock.Type = (BlockType)MainTetris.Random.Next(1, 8);
            MainTetris.Piece = new List<Block>();
            foreach (var block in MainTetris.Piece)
            {
                MainTetris.Blocks[block.X, block.Y].Type = BlockType.Empty;
            }

            MainTetris.Piece.Clear();

            switch (MainTetris.CurrentBlock.Type)
            {
                case BlockType.I:
                    for (int i = 0; i < 4; i++)
                    {
                        int newX = MainTetris.PositionX + i;
                        int newY = MainTetris.PositionY;
                        MainTetris.Piece.Add(new Block() { X = newX, Y = newY, Type = BlockType.I });
                        MainTetris.Blocks[newX, newY].Type = BlockType.I;
                    }
                    break;
                case BlockType.J:
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY, Type = BlockType.J });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY + 1, Type = BlockType.J });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY + 1, Type = BlockType.J });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 2, Y = MainTetris.PositionY + 1, Type = BlockType.J });
                    foreach (var block in MainTetris.Piece)
                    {
                        MainTetris.Blocks[block.X, block.Y].Type = BlockType.J;
                    }
                    break;
                case BlockType.L:
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 2, Y = MainTetris.PositionY, Type = BlockType.L });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY + 1, Type = BlockType.L });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY + 1, Type = BlockType.L });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 2, Y = MainTetris.PositionY + 1, Type = BlockType.L });
                    foreach (var block in MainTetris.Piece)
                    {
                        MainTetris.Blocks[block.X, block.Y].Type = BlockType.L;
                    }
                    break;
                case BlockType.O:
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY, Type = BlockType.O });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY, Type = BlockType.O });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY + 1, Type = BlockType.O });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY + 1, Type = BlockType.O });
                    foreach (var block in MainTetris.Piece)
                    {
                        MainTetris.Blocks[block.X, block.Y].Type = BlockType.O;
                    }
                    break;
                case BlockType.S:
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY, Type = BlockType.S });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 2, Y = MainTetris.PositionY, Type = BlockType.S });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY + 1, Type = BlockType.S });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY + 1, Type = BlockType.S });
                    foreach (var block in MainTetris.Piece)
                    {
                        MainTetris.Blocks[block.X, block.Y].Type = BlockType.S;
                    }
                    break;
                case BlockType.T:
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY, Type = BlockType.T });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY + 1, Type = BlockType.T });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY + 1, Type = BlockType.T });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 2, Y = MainTetris.PositionY + 1, Type = BlockType.T });
                    foreach (var block in MainTetris.Piece)
                    {
                        MainTetris.Blocks[block.X, block.Y].Type = BlockType.T;
                    }
                    break;
                case BlockType.Z:
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX, Y = MainTetris.PositionY, Type = BlockType.Z });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY, Type = BlockType.Z });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 1, Y = MainTetris.PositionY + 1, Type = BlockType.Z });
                    MainTetris.Piece.Add(new Block() { X = MainTetris.PositionX + 2, Y = MainTetris.PositionY + 1, Type = BlockType.Z });
                    foreach (var block in MainTetris.Piece)
                    {
                        MainTetris.Blocks[block.X, block.Y].Type = BlockType.Z;
                    }
                    break;
                default:
                    break;
            }

            foreach (var block in MainTetris.Piece)
            {
                if (MainTetris.Blocks[block.X, block.Y].IsLocked)
                {
                    GameOver();
                    return;
                }
                MainTetris.DrawBlock(block.X, block.Y);
            }
        }



        public void Move(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (MainTetris.PositionX > 0)
                    {
                        MoveToDirection(Direction.Left);
                    }
                    break;
                case Key.Right:
                    if (MainTetris.PositionX < MainTetris.Width - 1)
                    {
                        MoveToDirection(Direction.Right);
                    }
                    break;
                case Key.Down:
                    if (MainTetris.PositionY < MainTetris.Height - 1)
                    {
                        MoveToDirection(Direction.Down);
                    }
                    break;
                case Key.Up:
                    //Rotate();
                    break;
                default:
                    break;
            }
        }

        public void MoveToDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (MainTetris.PositionX >= 0 && MainTetris.PositionX <= MainTetris.Width - 1)
                    {
                        for (int i = 0; i < MainTetris.Piece.Count; i++)
                        {
                            var block = MainTetris.Piece[i];
                            if (block.X == 0 || MainTetris.Blocks[block.X - 1, block.Y].IsLocked)
                            {
                                return;
                            }
                        }
                        for (int i = 0; i < MainTetris.Piece.Count; i++)
                        {
                            var block = MainTetris.Piece[i];
                            MainTetris.Blocks[block.X, block.Y].Type = BlockType.Empty;
                            MainTetris.DrawBlock(block.X, block.Y);
                            block.X--;
                            MainTetris.Blocks[block.X, block.Y].Type = MainTetris.CurrentBlock.Type;
                            MainTetris.DrawBlock(block.X, block.Y);
                        }
                        MainTetris.PositionX--;
                    }
                    break;
                case Direction.Right:
                    if (MainTetris.PositionX < MainTetris.Width - 1 && MainTetris.PositionX >= 0)
                    {
                        for (int i = 0; i < MainTetris.Piece.Count; i++)
                        {
                            var block = MainTetris.Piece[i];
                            if (block.X == MainTetris.Width - 1 || MainTetris.Blocks[block.X + 1, block.Y].IsLocked)
                            {
                                return;
                            }
                        }
                        for (int i = MainTetris.Piece.Count - 1; i >= 0; i--)
                        {
                            var block = MainTetris.Piece[i];
                            MainTetris.Blocks[block.X, block.Y].Type = BlockType.Empty;
                            MainTetris.DrawBlock(block.X, block.Y);
                            block.X++;
                            MainTetris.Blocks[block.X, block.Y].Type = MainTetris.CurrentBlock.Type;
                            MainTetris.DrawBlock(block.X, block.Y);
                        }
                        MainTetris.PositionX++;
                    }
                    break;

                case Direction.Down:
                    if (MainTetris.PositionY < MainTetris.Height - 1)
                    {
                        MainTetris.PositionY++;
                        for (int i = 0; i < MainTetris.Piece.Count; i++)
                        {
                            var block = MainTetris.Piece[i];
                            if (block.Y == MainTetris.Height - 1 || MainTetris.Blocks[block.X, block.Y + 1].IsLocked)
                            {
                                LockPiece();
                                CreatePiece();
                                return;
                            }
                        }
                        for (int i = MainTetris.Piece.Count - 1; i >= 0; i--)
                        {
                            var block = MainTetris.Piece[i];
                            MainTetris.Blocks[block.X, block.Y].Type = BlockType.Empty;
                            MainTetris.DrawBlock(block.X, block.Y);
                            block.Y++;
                            MainTetris.Blocks[block.X, block.Y].Type = MainTetris.CurrentBlock.Type;
                            MainTetris.DrawBlock(block.X, block.Y);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void LockPiece()
        {
            foreach (var block in MainTetris.Piece)
            {
                MainTetris.Blocks[block.X, block.Y].IsLocked = true;
            }
        }

        public void GameOver()
        {
            MessageBox.Show("Game Over");
        }
    }
}
