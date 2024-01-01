using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
            Stopwatch stopwatch = Stopwatch.StartNew();
            int[] player = new int[2] { 60, 14 };
            int[] oldplayer = new int[2] { 0, 0 };
            int[] oldprey = new int[2] { 0, 0 };
            int[] prey = new int[2] { 100, 10};
            int[] hunter = new int[2] { 20, 10 };
            int[] hunter2 = new int[2] { 20, 20 };
            int score = 0;
            if (level > LevelMax) level = LevelMax;
            Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level);
            GenerateMap();
            int[] isGameOver = new int[1] { 187 };
            while (true)
            {
                oldplayer[0] = player[0];
                oldplayer[1] = player[1];
                MoveHunter();
                Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level);
                score = CheckCollision(player, prey, oldprey, hunter, hunter2, score, isGameOver, level, stopwatch);
                MovePrey(prey, oldprey, stopwatch, level);
                Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level);
                score = CheckCollision(player, prey, oldprey, hunter, hunter2, score, isGameOver, level, stopwatch);
                CheckPlayerImput(player);
                MovePlayer(player, oldplayer);
                Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level);
                score = CheckCollision(player, prey, oldprey, hunter, hunter2, score, isGameOver, level, stopwatch);
                if (isGameOver[0] == 0) { break; }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
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

            if (player[0] == 60)
            {
                if (player[1] >= 19)
                {
                    player[0] = oldplayer[0];
                    player[1] = oldplayer[1];
                }

                if (player[1] <= 9)
                {
                    player[0] = oldplayer[0];
                    player[1] = oldplayer[1];
                }
            }

            if (player[1] == 14)
            {
                if (player[0] >= 70) 
                {
                    player[0] = oldplayer[0];
                    player[1] = oldplayer[1];
                }

                if (player[0] <= 50)
                {
                    player[0] = oldplayer[0];
                    player[1] = oldplayer[1];
                }
            }
        }
        private void MoveHunter()
        {

        }
        private void MovePrey(int[] prey, int[] oldprey, Stopwatch stopwatch, int level)
        {
            long lastMovement = stopwatch.ElapsedMilliseconds;
            oldprey[0] = prey[0];
            oldprey[1] = prey[1];
            if (lastMovement >= 5000 - level * 500)
            {
                Random rand = new Random();
                prey[1] = rand.Next(2, 28);
                prey[0] = rand.Next(8, 114);
                stopwatch.Restart();
            }
        }
        private int CheckCollision(int[] player, int[] prey, int[] oldprey, int[] hunter, int[] hunter2, int score, int[] isGameOver, int level, Stopwatch stopwatch)
        {
            for (int j = 0; j < level * 2 + 2; j++)
            {
                if (hunter[0] + j == player[0]) 
                {
                    for (int d = 0; d < level * 2; d++)
                    {
                        if (hunter[1] + d == player[1])
                        {
                            isGameOver[0] = 0;
                        }
                    }
                }
            }
            for (int j = 0; j < level * 2 + 2; j++)
            {
                if (hunter2[0] + j == player[0])
                {
                    for (int d = 0; d < level * 2; d++)
                    {
                        if (hunter2[1] + d == player[1])
                        {
                            isGameOver[0] = 0;
                        }
                    }
                }
            }
            for (int i = -1; i < 2; i++)
            {
               
                if (prey[0] + i == player[0])
                {
                    
                    if (prey[1] == player[1])
                    {
                        oldprey[1] = prey[1];
                        oldprey[0] = prey[0];
                        Random rand = new Random();
                        prey[1] = rand.Next(2, 28);
                        prey[0] = rand.Next(8, 114);
                        stopwatch.Restart();
                        score++;
                        return score;
                    }

                }
            }
            return score;
        }
        private void Display(int[] player, int[] oldplayer, int[] prey, int[] oldprey, int[] hunter, int[] hunter2, int score, int level)
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
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            int z = 1;

                for (int i = 0; i < level; i++)
                {
                    z = z * 100;
                }
            Console.SetCursorPosition(hunter[0], hunter[1]);
            Console.Write("h" + z);
            Console.SetCursorPosition(hunter[0], hunter[1] + 1);
            Console.Write("h" + z);
            Console.SetCursorPosition(hunter2[0], hunter2[1]);
            Console.Write("h" + z);
            Console.SetCursorPosition(hunter2[0], hunter2[1] + 1);
            Console.Write("h" + z);
            Console.SetCursorPosition(hunter[0], hunter[1]);
            Console.SetCursorPosition(prey[0], prey[1]);
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

            for (int i = 1; i < 10; i++)
            {
                Console.SetCursorPosition(60, i);
                Console.Write("--");
            }
            for (int i = 19; i < 28; i++)
            {
                Console.SetCursorPosition(60, i);
                Console.Write("--");
            }

            for (int i = 70; i < 117; i++)
            {
                Console.SetCursorPosition(i, 14);
                Console.Write("-");
            }
            for (int i = 6; i < 51; i++)
            {
                Console.SetCursorPosition(i, 14);
                Console.Write("--");
            }
        }

    }
}
