using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public static class Main
    {
        private static HashSet<Character> characters = [];
        private static HashSet<string> imageURLs = [];
        private static HashSet<string> randomNames = [];
        private static Random random = new();

        public static HashSet<Character> Characters { get => characters; set => characters = value; }
        public static HashSet<string> ImageURLs { get => imageURLs; set => imageURLs = value; }
        public static HashSet<string> RandomNames { get => randomNames; set => randomNames = value; }
        public static Random Random { get => random; set => random = value; }

        public static void LoadResources()
        {
            ImageURLs = [];
            RandomNames = [];

            for (int i = 1; i <= 14; i++)
            {
                // Do not touch - it took me 7 hours to get it working
                ImageURLs.Add($@"pack://application:,,,/RPG;component\Characters\char{i}.png");
            }

            RandomNames = [.. File.ReadAllLines("RandomNames.txt")];
        }
    }
}
