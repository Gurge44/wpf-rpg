using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RPG
{
    internal static class MainFight
    {
        public static Random random = new Random();
        public static Block[,] Blocks;
        public static int Width;
        public static int Height;
        public static int Level;
        public static int Experience;
        public static int currentHealth;
        public static int currentDamage;
        public static int enemyHealth;
        public static int enemyDamage;
        public static int characterHealth;
        public static int characterDamage;
        private static Fight FightInstance;
        public static int time;
        
        public static void Fightinstance(Fight fight)
        {
            FightInstance = fight;
        }

        public static void DrawBlock(int x, int y)
        {
            BlockType type = Blocks[x, y].Type;
            switch (type)
            {
                case BlockType.Empty:
                    Blocks[x, y].Button.Background = Brushes.White;
                    break;
                case BlockType.HealthBoost:
                    Blocks[x, y].Button.Background = Brushes.Green;
                    break;
                case BlockType.DamageBoost:
                    Blocks[x, y].Button.Background = Brushes.Red;
                    break;
                case BlockType.HealthEffect:
                    Blocks[x, y].Button.Background = Brushes.Yellow;
                    break;
                case BlockType.DamageEffect:
                    Blocks[x, y].Button.Background = Brushes.Blue;
                    break;
            }

        }

        public static void HandleType(int x, int y)
        {
            BlockType type = Blocks[x, y].Type;
            switch (type)
            {
                case BlockType.Empty:
                    break;
                case BlockType.HealthBoost:
                    currentHealth += 10;
                    break;
                case BlockType.DamageBoost:
                    currentDamage += 10;
                    break;
                case BlockType.HealthEffect:
                    switch(random.Next(1, 5))
                    {
                        case 1:
                            currentHealth /= 2;
                            break;
                        case 2:
                            currentHealth *= 2;
                            break;
                        case 3:
                            currentHealth /= 3;
                            break;
                        case 4:
                            currentHealth *= 3;
                            break;
                    }
                    break;
                case BlockType.DamageEffect:
                    switch (random.Next(1, 5))
                    {
                        case 1:
                            currentDamage /= 2;
                            break;
                        case 2:
                            currentDamage *= 2;
                            break;
                        case 3:
                            currentDamage /= 3;
                            break;
                        case 4:
                            currentDamage *= 3;
                            break;
                    }
                    break;
                case BlockType.Kill:
                    switch(random.Next(1, 3))
                    {
                        case 1:
                            currentHealth *= 0;
                            break;
                        case 2:
                            enemyHealth = 0;
                            
                            break;
                    }
                    break;
            }
            Blocks[x, y].Type = BlockType.Empty;
        }

        public static BlockType SetRandomType(int x, int y)
        {
            int q = random.Next(0, 5000);
            if (q == 1)
            {
                return Blocks[x, y].Type = BlockType.Kill;

            }
            int randomType = random.Next(0, 5);
            switch (randomType)
            {
                case 0:
                    return Blocks[x, y].Type = BlockType.Empty;
                case 1:
                    return Blocks[x, y].Type = BlockType.HealthBoost;
                case 2:
                    return Blocks[x, y].Type = BlockType.DamageBoost;
                case 3:
                    return Blocks[x, y].Type = BlockType.HealthEffect;
                case 4:
                    return Blocks[x, y].Type = BlockType.DamageEffect;
            }
            return Blocks[x, y].Type = BlockType.Empty;
        }

        public static void EndCurrentLevel()
        {
            Level++;
            Experience += 10;
            Width += 5;
            Height += 5;
        }

        public static void EndCurrentAttack()
        {
            enemyHealth -= currentDamage;
            characterHealth -= currentHealth;
            if (enemyHealth <= 0)
            {
                EndCurrentLevel();
            }
            if (characterHealth <= 0)
            {
                FightInstance.GameOver();
            }
        }

        
    }
}
