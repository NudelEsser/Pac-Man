using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGames.Games
{
    internal class Pac_Man : Game
    {
        public override string Name => "Pac Man";
        public override string Description => "Sammle das magische Gold ein und weiche den Jägern der Nacht aus. Du benötigst 20 Gold um das nächste Level zu betreten.";
        public override string Rules => "Bewege dich mit W A S D";
        public override string Credits => "Luca Osti, lucaosti@ksr.ch";
        public override int Year => 2023;
        public override bool TheHigherTheBetter => true;
        public override int LevelMax => 3;
        public override Score HighScore { get; set; }
        public override Score Play(int level = 1)
        {
            Score TrueScore = new Score();
            TrueScore.Level = level;
            Console.CursorVisible = false;
            Stopwatch stopwatch = Stopwatch.StartNew();
            Stopwatch hunterwatch = Stopwatch.StartNew();
            int[] player = new int[2] { 60, 14 };
            int[] oldplayer = new int[2] { 0, 0 };
            int[] oldprey = new int[2] { 0, 0 };
            int[] prey = new int[2] { 112, 3};
            int[] hunter = new int[2] { 20, 6 };
            int[] hunter2 = new int[2] { 20, 19 };
            int[] hunter3 = new int[2] { 100, 6 };
            int[] hunter4 = new int[2] { 100, 19 };
            int[] oldhunter = new int[] { 108, 23 };
            int[] oldhunter2 = new int[] { 108, 23 };
            int[] oldhunter3 = new int[] { 108, 23 };
            int[] oldhunter4 = new int[] { 108, 23 };
            if (level == 2)
            {
                hunter[1] = 5;
                hunter2[1] = 18;
                hunter3[1] = 5;
                hunter4[1] = 18;
            }
            int score = 0;
            if (level > LevelMax) level = LevelMax;
            Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level, hunter3, hunter4, oldhunter, oldhunter2, oldhunter3, oldhunter4);
            GenerateMap(level);
            int[] isGameOver = new int[1] { 187 };
            while (true)
            {
                oldplayer[0] = player[0];
                oldplayer[1] = player[1];
                MoveHunter(player, hunter, hunter2, hunter3, hunter4, level, hunterwatch, oldhunter, oldhunter2, oldhunter3, oldhunter4);
                Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level, hunter3, hunter4, oldhunter, oldhunter2, oldhunter3, oldhunter4);
                score = CheckCollision(player, prey, oldprey, hunter, hunter2, score, isGameOver, level, stopwatch, hunter3, hunter4);
                MovePrey(prey, oldprey, stopwatch, level);
                Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level, hunter3, hunter4, oldhunter, oldhunter2, oldhunter3, oldhunter4);
                score = CheckCollision(player, prey, oldprey, hunter, hunter2, score, isGameOver, level, stopwatch, hunter3, hunter4);
                CheckPlayerImput(player);
                MovePlayer(player, oldplayer, level);
                Display(player, oldplayer, prey, oldprey, hunter, hunter2, score, level, hunter3, hunter4, oldhunter, oldhunter2, oldhunter3, oldhunter4);
                score = CheckCollision(player, prey, oldprey, hunter, hunter2, score, isGameOver, level, stopwatch, hunter3, hunter4);
                if (isGameOver[0] == 0) 
                {
                    int answer = quitOrRestart();
                    if (answer == 0)
                    {
                        break;
                    }
                    if (answer == 1)
                    {
                        hunter = new int[2] { 20, 6 };
                        hunter2 = new int[2] { 20, 19 };
                        hunter3 = new int[2] { 100, 6 };
                        hunter4 = new int[2] { 100, 19 };
                        if (level == 2)
                        {
                            hunter[1] = 5;
                            hunter2[1] = 18;
                            hunter3[1] = 5;
                            hunter4[1] = 18;
                        }
                        isGameOver[0] = 187;
                        player[0] = 60;
                        player[1] = 14;
                        Console.Clear();
                        GenerateMap(level);
                        score = 0;
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            TrueScore.Points = score;
            if (score >= 20)
            {
                TrueScore.LevelCompleted = true;
            }
            return TrueScore;
        }

        private void CheckPlayerImput(int[] player)     //view
        {
            if (Console.KeyAvailable)
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
        }
        private void MovePlayer(int[] player, int[] oldplayer, int level)
        {
            int special = 0;
            if (level == 2)
            {
                special = 2;
            }
            int special2 = 0;
            if (level == 3)
            {
                special2 = 4;
            }
            if (player[0] >= 116 + special)
            {
                player[0] = player[0] - 2;
            }
            if (player[0] <= 6 - special2)
            {
                player[0] = player[0] + 2;
            }
            if (player[1] >= 27)
            {
                player[1] = player[1] - 1;
            }
            if (player[1] <= 1)
            {
                player[1] = player[1] + 1;
            }

            if (player[0] == 60 + special)
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
                if (player[0] >= 70 + special) 
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
        private void MoveHunter(int[] player, int[] hunter, int[] hunter2, int[] hunter3, int[] hunter4, int level, Stopwatch hunterwatch, int[] oldhunter, int[] oldhunter2, int[] oldhunter3, int[] oldhunter4)
        {
            int special = 0;
            if (level == 2)
            {
                special = 2;
            }
            long lastMovement = hunterwatch.ElapsedMilliseconds;
            Random rand = new Random();
            if (lastMovement >= 200 + level * 150)
            {
                hunterwatch.Restart();
                if (player[1] <= 13)
                {
                    if (player[0] <= 58 + special)
                    {
                        MoveSeperateHunter(hunter, level, player, oldhunter);
                    }
                    if (player[0] >= 62 + special)
                    {
                        MoveSeperateHunter(hunter3, level, player, oldhunter3);
                    }

                }
                if (player[1] >= 15)
                {
                    if (player[0] <= 58 + special)
                    {
                        MoveSeperateHunter(hunter2, level, player, oldhunter2);
                    }
                    if (player[0] >= 62 + special)
                    {
                        MoveSeperateHunter(hunter4, level, player, oldhunter4);
                    }
                }
            }
            static void MoveSeperateHunter(int[] hunterX, int level, int[] player, int[] oldhunterX)
            {
                Random rand = new Random();
                int move = 0;

                oldhunterX[0] = hunterX[0];
                oldhunterX[1] = hunterX[1];
                move = rand.Next(0, 2);
                if (move == 0)
                {
                    if (hunterX[0] > player[0])
                    {
                        hunterX[0] = hunterX[0] - 2 - level * 2;
                    }
                    else if (hunterX[0] < player[0] - level * 2)
                    {
                        hunterX[0] = hunterX[0] + 2 + level * 2;
                    }
                    else
                    {
                        move = 1;
                    }
                }
                if (move == 1)
                {
                    if (hunterX[1] > player[1])
                    {
                        hunterX[1] = hunterX[1] - 1 - level;
                    }
                    else if (hunterX[1] < player[1] - level)
                    {
                        hunterX[1] = hunterX[1] + 1 + level;
                    }
                    else
                    {
                        if (hunterX[0] > player[0])
                        {
                            hunterX[0] = hunterX[0] - 2 - level * 2;
                        }
                        else if (hunterX[0] < player[0] - level * 2)
                        {
                            hunterX[0] = hunterX[0] + 2 + level * 2;
                        }
                    }
                }
            }
        }
        private void MovePrey(int[] prey, int[] oldprey, Stopwatch stopwatch, int level)
        {
            int special = 0;
            if (level == 0)
            {
                special = 1;
            }
            long lastMovement = stopwatch.ElapsedMilliseconds;
            oldprey[0] = prey[0];
            oldprey[1] = prey[1];
            if (lastMovement >= 5000 - level * 500)
            {
                Random rand = new Random();
                int p = rand.Next(0, 4);
                if (p == 1)
                {
                    prey[1] = rand.Next(2, 13);
                    prey[0] = rand.Next(8, 58);
                }
                if (p == 2)
                {
                    prey[1] = rand.Next(15, 27);
                    prey[0] = rand.Next(8, 58);
                }
                if (p == 3)
                {
                    prey[1] = rand.Next(2, 13);
                    prey[0] = rand.Next(62 + special, 114);
                }
                if (p == 0)
                {
                    prey[1] = rand.Next(15, 27);
                    prey[0] = rand.Next(62 + special, 114);
                }
                stopwatch.Restart();
            }
        }
        private int CheckCollision(int[] player, int[] prey, int[] oldprey, int[] hunter, int[] hunter2, int score, int[] isGameOver, int level, Stopwatch stopwatch, int[] hunter3, int[] hunter4)
        {
            for (int j = 0; j < level * 2 + 2; j++)
            {
                if (hunter[0] + j == player[0]) 
                {
                    for (int d = 0; d < level + 1; d++)
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
                    for (int d = 0; d < level + 1; d++)
                    {
                        if (hunter2[1] + d == player[1])
                        {
                            isGameOver[0] = 0;
                        }
                    }
                }
            }
            for (int j = 0; j < level * 2 + 2; j++)
            {
                if (hunter3[0] + j == player[0])
                {
                    for (int d = 0; d < level + 1; d++)
                    {
                        if (hunter3[1] + d == player[1])
                        {
                            isGameOver[0] = 0;
                        }
                    }
                }
            }
            for (int j = 0; j < level * 2 + 2; j++)
            {
                if (hunter4[0] + j == player[0])
                {
                    for (int d = 0; d < level + 1; d++)
                    {
                        if (hunter4[1] + d == player[1])
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
                        int p = rand.Next(0, 4);
                        if ( p == 1)
                        {
                            prey[1] = rand.Next(2, 13);
                            prey[0] = rand.Next(8, 58);
                        }
                        if (p == 2)
                        {
                            prey[1] = rand.Next(15, 27);
                            prey[0] = rand.Next(8, 58);
                        }
                        if (p == 3)
                        {
                            prey[1] = rand.Next(2, 13);
                            prey[0] = rand.Next(62, 114);
                        }
                        if (p == 0)
                        {
                            prey[1] = rand.Next(15, 27);
                            prey[0] = rand.Next(62, 114);
                        }

                        stopwatch.Restart();
                        score++;
                        return score;
                    }

                }
            }
            return score;
        }
        private int quitOrRestart()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Game over! Quit or restart? (q/r)");
            while (true)
            {
                char reply = Console.ReadKey(true).KeyChar;
                if (reply == 'q') 
                {
                    return 0;
                }
                if (reply == 'r')
                {
                    return 1;
                }
                Console.Clear ();
                Console.WriteLine("Game over! Quit or restart? (q/r)");
                Console.WriteLine("please reply with 'q' to quit or 'r' to restart. ");
            }
        }
        private void Display(int[] player, int[] oldplayer, int[] prey, int[] oldprey, int[] hunter, int[] hunter2, int score, int level, int[] hunter3, int[] hunter4, int[] oldhunter, int[] oldhunter2, int[] oldhunter3, int[] oldhunter4)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.Write("  Gold: " + score);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            if (player[1] != oldplayer[1])
            {
                Console.SetCursorPosition(oldplayer[0], oldplayer[1]);
                Console.Write("OO");
            }
            if (player[0] != oldplayer[0])
            {
                Console.SetCursorPosition(oldplayer[0], oldplayer[1]);
                Console.Write("OO");
            }
            if (prey[1] != oldprey[1])
            {
                Console.SetCursorPosition(oldprey[0], oldprey[1]);
                Console.Write("OO");
            }
            if (prey[0] != oldprey[0])
            {
                Console.SetCursorPosition(oldprey[0], oldprey[1]);
                Console.Write("OO");
            }

            int z = 1;
            for (int i = 0; i < level; i++)
            {
                z = z * 100;
            }
            if (hunter[0] != oldhunter[0])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter[0], oldhunter[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter[1] != oldhunter[1])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter[0], oldhunter[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter2[0] != oldhunter2[0])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter2[0], oldhunter2[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter2[1] != oldhunter2[1])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter2[0], oldhunter2[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter3[0] != oldhunter3[0])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter3[0], oldhunter3[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter3[1] != oldhunter3[1])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter3[0], oldhunter3[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter4[0] != oldhunter4[0])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter4[0], oldhunter4[1] + i);
                    Console.Write("0" + z);
                }
            }
            if (hunter4[1] != oldhunter4[1])
            {
                for (int i = 0; i < level + 1; i++)
                {
                    Console.SetCursorPosition(oldhunter4[0], oldhunter4[1] + i);
                    Console.Write("0" + z);
                }
            }
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
            for (int i = 0; i < level + 1; i++)
            {
                Console.SetCursorPosition(hunter[0], hunter[1] + i );
                Console.Write("h" + z);
            }
            for (int i = 0; i < level + 1; i++)
            {
                Console.SetCursorPosition(hunter2[0], hunter2[1] + i);
                Console.Write("h" + z);
            }
            for (int i = 0; i < level + 1; i++)
            {
                Console.SetCursorPosition(hunter3[0], hunter3[1] + i);
                Console.Write("h" + z);
            }
            for (int i = 0; i < level + 1; i++)
            {
                Console.SetCursorPosition(hunter4[0], hunter4[1] + i);
                Console.Write("h" + z);
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        private void GenerateMap(int level)
        {
            int special = 0;
            if (level == 2)
            {
                special = 2;
            }
            int special2 = 0;
            if (level == 3)
            {
                special2 = 4;
            }
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 1; i < 28; i++)
            {
                Console.SetCursorPosition(6 - special2, i);
                Console.Write("--");
            }
            for (int i = 1; i < 28; i++)
            {
                Console.SetCursorPosition(116 + special, i);
                Console.Write("--");
            }
            for (int i = 6 - special2; i < 117; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("--");
            }
            for (int i = 6 - special2; i < 117; i++)
            {
                Console.SetCursorPosition(i, 27);
                Console.Write("--");
            }

            for (int i = 1; i < 10; i++)
            {
                Console.SetCursorPosition(60 + special, i);
                Console.Write("--");
            }
            for (int i = 19; i < 28; i++)
            {
                Console.SetCursorPosition(60 + special, i);
                Console.Write("--");
            }

            for (int i = 70 + special; i < 117; i++)
            {
                Console.SetCursorPosition(i, 14);
                Console.Write("--");
            }
            for (int i = 6 - special2; i < 51; i++)
            {
                Console.SetCursorPosition(i, 14);
                Console.Write("--");
            }
        }

    }
}
