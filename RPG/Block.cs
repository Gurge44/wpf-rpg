using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace RPG
{
    public class Block
    {
        public BlockType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Rectangle Rectangle { get; set; }
    }

    public enum BlockType
    {
        Empty,
        Piece,
        Border,
    }

    //tetris
    public class Piece
    {
        public PieceType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Rotation { get; set; }
        public List<Block> Blocks { get; set; }
    }

    public enum PieceType
    {
        I,
        J,
        L,
        O,
        S,
        T,
        Z
    }

}
