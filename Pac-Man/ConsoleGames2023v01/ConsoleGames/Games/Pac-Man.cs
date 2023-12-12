using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGames.Games
{
    internal class Pac_Man : Game
    {
        public override string Name => "Pac Man";
        public override string Description => "Gegeben ist eine Zahl zwischen 1 und 100 (inkl. Grenzen).\nErrate diese Zahl.";
        public override string Rules => "Bewege dich mit W A S D";
        public override string Credits => "Luca Osti, lucaosti@ksr.ch";
        public override int Year => 2023;
        public override bool TheHigherTheBetter => true;
        public override int LevelMax => 400;
        public override Score HighScore { get; set; }

        public override Score Play(int level = 1)
        {
            int[] player = new int[2] { 50, 4 };
            int[] oldplayer = new int[2] { 0, 0 };
            int[] prey = new int[2] { 1, 1};
            List<int[]> scores = new List<int[]>();
            List<int[]> path = new List<int[]>();
            int timer = 60000;
            Display(player, oldplayer);
            GenerateMap();
            while (timer > 0)
            {
                oldplayer[0] = player[0];
                oldplayer[1] = player[1];
                MoveHunter();
                Display(player, oldplayer);
                CheckCollision();
                MovePrey();
                Display(player, oldplayer);
                CheckCollision();
                CheckPlayerImput(player);
                MovePlayer(player);
                Display(player, oldplayer);
                CheckCollision();
                timer--;
                if (timer == 0)
                {
                    break;
                }
            }
            return new Score();

            
        }

        private void CheckPlayerImput(int[] player)     //view
        {
            char move = Console.ReadKey(true).KeyChar;
            if (move == 'w')
            {
                player[1] = player[1] - 1;
            }
            if (move == 'a')
            {
                player[0] = player[0] - 2;
            }
            if (move == 's')
            {
                player[1] = player[1] + 1;
            }
            if (move == 'd')
            {
                player[0] = player[0] + 2;
            }
        }
        private void MovePlayer(int[] player)
        {
            if (player[0] >= 116)
            {
                player[0] = player[0] - 2;
            }
            if (player[0] <= 6)
            {
                player[0] = player[0] + 2;
            }
            if (player[1] >= 28)
            {
                player[1] = player[1] - 1;
            }
            if (player[1] <= 1)
            {
                player[1] = player[1] + 1;
            }
            Console.SetCursorPosition(player[0], player[1]);
        }
        private void MoveHunter()
        {

        }
        private void MovePrey()
        {

        }
        private void CheckCollision()
        {

        }
        private void Display(int[] player, int[] oldplayer)
        {
            Console.SetCursorPosition(oldplayer[0], oldplayer[1]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("OO");
            Console.SetCursorPosition(player[0], player[1]);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("PP");

        }
        private void GenerateMap()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 1; i < 28; i++)
            {
                Console.SetCursorPosition(6, i);
                Console.Write("--");
            }
            for (int i = 1; i < 28; i++)
            {
                Console.SetCursorPosition(116, i);
                Console.Write("--");
            }
            for (int i = 6; i < 117; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }
            for (int i = 6; i < 117; i++)
            {
                Console.SetCursorPosition(i, 28);
                Console.Write("--");
            }
        }

    }
}
