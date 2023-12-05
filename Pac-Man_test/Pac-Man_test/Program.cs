namespace Pac_Man_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] player = new int[2] { 50, 4 };
            int[] oldplayer = new int[2] { 0, 0 };
            int[] prey = new int[2] { 1, 1 };
            List<int[]> scores = new List<int[]>();
            List<int[]> path = new List<int[]>();
            int timer = 60000;
            Display(player, oldplayer);
            Console.SetCursorPosition(player[0], player[1]);
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
                //timer--;
                if (timer == 0)
                {
                    break;
                }
            }
        }
         private static void CheckPlayerImput(int[] player)     //view
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
        private static void MovePlayer(int[] player)
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
        private static void MoveHunter()
        {

        }
        private static void MovePrey()
        {

        }
        private static void CheckCollision()
        {

        }
        private static void Display(int[] player, int[] oldplayer)
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
            Console.SetCursorPosition(oldplayer[0], oldplayer[1]);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("OO");
            Console.SetCursorPosition(player[0], player[1]);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("PP");

        }
    }
}