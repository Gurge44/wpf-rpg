using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RPG
{
    public class Block
    {
        public BlockType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Button Button { get; set; }
    }

    public enum BlockType
    {
        Empty,
        HealthBoost,
        DamageBoost,
        HealthEffect,
        DamageEffect,
        Kill
    }
}
