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
        public static Character Currentenemy = Main.Enemies.RandomElement();
        
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
            characterHealth -= enemyDamage;
            if (enemyHealth <= 0)
            {
                EndCurrentLevel();
            }
            if (characterHealth <= 0)
            {
                FightInstance.GameOver();
            }
        }

        public static void SetCharacterDamage(Character character)
        {
            characterDamage = character.Strength switch
            {
                SkillLevel.VeryLow => random.Next(10, 20),
                SkillLevel.Low => random.Next(20, 30),
                SkillLevel.Average => random.Next(30, 40),
                SkillLevel.High => random.Next(40, 50),
                SkillLevel.VeryHigh => random.Next(50, 60),
                _ => 0,
            };
            characterDamage *= Level;
            characterDamage += random.Next(20, 40);
        }

        public static void SetCharacterHealth(Character character)
        {
            characterHealth = character.Vitality switch
            {
                SkillLevel.VeryLow => random.Next(100, 150),
                SkillLevel.Low => random.Next(150, 200),
                SkillLevel.Average => random.Next(200, 250),
                SkillLevel.High => random.Next(250, 300),
                SkillLevel.VeryHigh => random.Next(300, 350),
                _ => 0,
            };
            characterHealth *= random.Next(1, 3);
        }

        public static void SetEnemyDamage(Character enemy)
        {
            enemyDamage = enemy.Strength switch
            {
                SkillLevel.VeryLow => random.Next(10, 20),
                SkillLevel.Low => random.Next(20, 30),
                SkillLevel.Average => random.Next(30, 40),
                SkillLevel.High => random.Next(40, 50),
                SkillLevel.VeryHigh => random.Next(50, 60),
                _ => 0,
            };
            enemyDamage *= Level * random.Next(1, Level + 8);
        }

        public static void SetEnemyHealth(Character enemy)
        {
            enemyHealth = enemy.Vitality switch
            {
                SkillLevel.VeryLow => random.Next(100, 150),
                SkillLevel.Low => random.Next(150, 200),
                SkillLevel.Average => random.Next(200, 250),
                SkillLevel.High => random.Next(250, 300),
                SkillLevel.VeryHigh => random.Next(300, 350),
                _ => 0,
            };
            enemyHealth *= (int)Math.Round(Math.Sin(Level) * 2 * Math.PI * 13 - 64 + 1);
        }
    }
}
