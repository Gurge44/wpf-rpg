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
        public bool IsLocked { get; set; }
        public bool Rotated { get; set; }

    }

    public enum BlockType
    {
        Empty,
        I,
        J,
        L,
        O,
        S,
        T,
        Z,
        Wall
    }

    public enum Direction
    {
        Left,
        Right,
        Down
    }
}
