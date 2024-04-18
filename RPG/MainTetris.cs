using System.Windows.Media;

namespace RPG
{
    internal static class MainTetris
    {

        public static Random random = new Random();
        public static Block[,] Blocks;
        public static int Width;
        public static int Height;
        public static int Score;
        public static int Lines;
        public static int Level;
        public static int LinesCleared;
        public static Piece CurrentPiece;
        public static Piece NextPiece;
        public static bool GameOver = false;
        public static bool Paused = false;
        public static bool Started = false;
        public static bool IsNewPiece = false;
        public static bool IsNewGame = true;
        public static bool IsGameOver = false;
        public static bool IsPaused = false;
        public static bool IsStarted = false;
        private static Fight Instance;


        public static void FightInstance(Fight fight)
        {
            Instance = fight;
        }


        public static void DrawBlock(int x, int y)
        {
            BlockType type = Blocks[x, y].Type;
            switch (type)
            {
                case BlockType.Empty:
                    Blocks[x, y].Rectangle.Fill = Brushes.White;
                    break;
                case BlockType.Piece:
                    switch(CurrentPiece.Type)
                        {                         
                        case PieceType.I:
                            Blocks[x, y].Rectangle.Fill = Brushes.Cyan;
                            break;
                        case PieceType.J:
                            Blocks[x, y].Rectangle.Fill = Brushes.Blue;
                            break;
                        case PieceType.L:
                            Blocks[x, y].Rectangle.Fill = Brushes.Orange;
                            break;
                        case PieceType.O:
                            Blocks[x, y].Rectangle.Fill = Brushes.Yellow;
                            break;
                        case PieceType.S:
                            Blocks[x, y].Rectangle.Fill = Brushes.Green;
                            break;
                        case PieceType.T:
                            Blocks[x, y].Rectangle.Fill = Brushes.Purple;
                            break;
                        case PieceType.Z:
                            Blocks[x, y].Rectangle.Fill = Brushes.Red;
                            break;
                    }
                    break;
                case BlockType.Border:
                    Blocks[x, y].Rectangle.Fill = Brushes.Black;
                    break;
            }
        }

        public static void SetBlock(BlockType type, int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;
            Blocks[x, y].Type = type;
            DrawBlock(x, y);
        }

        public static void SetPieceBlocks(int x, int y)
        {
            foreach (Block block in CurrentPiece.Blocks)
            {
                SetBlock(BlockType.Empty, block.X, block.Y);
            }
            foreach (Block block in CurrentPiece.Blocks)
            {
                SetBlock(BlockType.Piece, block.X + x, block.Y + y);
            }
        }
    }
}
