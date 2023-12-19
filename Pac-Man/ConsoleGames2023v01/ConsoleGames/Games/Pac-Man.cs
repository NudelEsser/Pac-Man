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
        public override int LevelMax => 8;
        public override Score HighScore { get; set; }
        public override Score Play(int level = 1)
        {

            int[] player = new int[2] { 50, 4 };
            int[] oldplayer = new int[2] { 0, 0 };
            int[] oldprey = new int[2] { 0, 0 };
            int[] prey = new int[2] { 100, 10};
            int score = 0;
            List<int[]> hunters = new List<int[]> 
            { 
                new int[] { 20, 20 },
            };
            int timer = 1000000;
            Display(player, oldplayer, prey, oldprey, hunters, score);
            GenerateMap();
            while (timer > 0)
            {
                oldplayer[0] = player[0];
                oldplayer[1] = player[1];
                MoveHunter();
                Display(player, oldplayer, prey, oldprey, hunters, score);
                score = CheckCollision(player, prey, oldprey, score);
                MovePrey();
                Display(player, oldplayer, prey, oldprey, hunters, score);
                score = CheckCollision(player, prey, oldprey, score);
                CheckPlayerImput(player);
                MovePlayer(player, oldplayer);
                Display(player, oldplayer, prey, oldprey, hunters, score);
                score = CheckCollision(player, prey, oldprey, score);
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
        private void MovePlayer(int[] player, int[] oldplayer)
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
        }
        private void MoveHunter()
        {

        }
        private void MovePrey()
        {

        }
        private int CheckCollision(int[] player, int[] prey, int[] oldprey, int score)
        {
            for (int i = -1; i < 2; i++)
            {

                if (prey[0] + i == player[0])
                {
                    if (prey[1] == player[1])
                    {
                        oldprey[1] = prey[1];
                        oldprey[0] = prey[0];
                        Random rand = new Random();
                        prey[1] = rand.Next(2, 27);
                        prey[0] = rand.Next(8, 114);

                        score++;
                        return score;
                    }
                }
            }
            return score;
        }
        private void Display(int[] player, int[] oldplayer, int[] prey, int[] oldprey, List<int[]> hunters, int score)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write("  Score: " + score);
            Console.SetCursorPosition(oldplayer[0], oldplayer[1]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("OO");
            Console.SetCursorPosition(oldprey[0], oldprey[1]);
            Console.Write("OO");
            Console.SetCursorPosition(player[0], player[1]);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Pl");
            Console.SetCursorPosition(prey[0], prey[1]);
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pr");
            for (int i = 0; i < hunters.Count; i++)
            {
                Console.SetCursorPosition(1, prey[1]);
            }
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
