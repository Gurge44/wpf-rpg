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
        public static ProgressBar enemyHprogressBar = new ProgressBar();
        public static ProgressBar characterHprogressBar = new ProgressBar();

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
            enemyHealth = 200 * level;
            enemyDamage = 10 * level;
            characterHealth = 1000;
            characterDamage = 20;
        }

        public static void FightOver()
        {
            ChooseToFight.Instance.Close();
        }
    }
}
