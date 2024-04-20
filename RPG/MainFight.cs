using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace RPG
{
    internal static class MainFight
    {
        public static Grid MainGrid;
        public static int enemyHealth;
        public static int enemyDamage;
        public static int characterHealth;
        public static int characterDamage;
        private static Fight FightInstance;
        public static int time;
        public static Character Currentenemy = Main.Enemies.RandomElement();
        public static Fight Instance;
        public static int Max_E;
        public static int Max_C;
        public static ProgressBar enemyHprogressBar;
        public static ProgressBar characterHprogressBar;
        public static bool first = true;

        public static void Fightinstance(Fight fight)
        {
            Instance = fight;
        }

        public static void EndCurrentAttack()
        {
            enemyHealth -= characterDamage;
            enemyHprogressBar.Value = enemyHealth;
            if (enemyHealth <= 0 || characterHealth <= 0)
            {
                FightOver();
                
            }
            characterHealth -= enemyDamage;
            characterHprogressBar.Value = characterHealth;
        }

        public static void SetDefaultValues(int level)
        {
            switch (Main.SelectedCharacter.Strength)
            {
                case SkillLevel.VeryLow:
                    characterHealth = 50;
                    characterDamage = 5;
                    break;
                case SkillLevel.Low:
                    characterHealth = 100;
                    characterDamage = 10;
                    break;
                case SkillLevel.Average:
                    characterHealth = 150;
                    characterDamage = 15;
                    break;
                case SkillLevel.High:
                    characterHealth = 200;
                    characterDamage = 20;
                    break;
                case SkillLevel.VeryHigh:
                    characterHealth = 250;
                    characterDamage = 25;
                    break;
            }

            switch (level)
            {
                case 1:
                    enemyHealth = 50;
                    enemyDamage = 5;
                    break;
                case 2:
                    enemyHealth = 100;
                    enemyDamage = 10;
                    break;
                case 3:
                    enemyHealth = 150;
                    enemyDamage = 15;
                    break;
                case 4:
                    enemyHealth = 200;
                    enemyDamage = 20;
                    break;
                case 5:
                    enemyHealth = 250;
                    enemyDamage = 25;
                    break;
                case 6:
                    enemyHealth = 300;
                    enemyDamage = 30;
                    break;
            }


        }

        public static void FightOver()
        {
            ChooseToFight.Instance.Close();
        }
    }
}
