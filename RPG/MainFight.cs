﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

        public static void Fightinstance(Fight fight)
        {
            Instance = fight;
        }

        public static void EndCurrentAttack()
        {
            enemyHealth -= 10;
            enemyHprogressBar.Value = enemyHealth;
            if (enemyHealth <= 0)
            {
                Main.Enemies.Remove(Currentenemy);
                if (Main.Enemies.Count == 0)
                {
                    FightOver();
                }
            }
        }

        public static void SetDefaultValues(int level)
        {
            enemyHealth = 200 * level;
            enemyDamage = 50 * level;
            characterHealth = 1000;
            characterDamage = 100;
        }

        public static void FightOver()
        {
            ChooseToFight.Instance.Close();
        }


    }
}
