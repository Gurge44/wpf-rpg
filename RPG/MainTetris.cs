using System.Windows.Media;

namespace RPG
{
    internal static class MainTetris
    {
        public static int GridHeight;
        public static int PositionX;
        public static int PositionY;
        public static Block[,] Blocks;
        public static Block CurrentBlock;
        public static Block NextBlock;
        public static int Width;
        public static int Height;
        public static Random Random;
        public static List<Block> Piece;
        private static MainWindow Instance;

        public static void MainWindowInstance(MainWindow instance)
        {
            Instance = instance;
        }

        public static void DrawBlock(int x, int y)
        {
            BlockType type = Blocks[x, y].Type;
            switch (type)
            {
                case BlockType.Empty:
                    Blocks[x, y].Rectangle.Fill = Brushes.White;
                    break;
                case BlockType.I:
                    Blocks[x, y].Rectangle.Fill = Brushes.Cyan;
                    break;
                case BlockType.J:
                    Blocks[x, y].Rectangle.Fill = Brushes.Blue;
                    break;
                case BlockType.L:
                    Blocks[x, y].Rectangle.Fill = Brushes.Orange;
                    break;
                case BlockType.O:
                    Blocks[x, y].Rectangle.Fill = Brushes.Yellow;
                    break;
                case BlockType.S:
                    Blocks[x, y].Rectangle.Fill = Brushes.Green;
                    break;
                case BlockType.T:
                    Blocks[x, y].Rectangle.Fill = Brushes.Purple;
                    break;
                case BlockType.Z:
                    Blocks[x, y].Rectangle.Fill = Brushes.Red;
                    break;
                case BlockType.Wall:
                    Blocks[x, y].Rectangle.Fill = Brushes.Gray;
                    break;
                default:
                    break;
            }
        }
    }
}

