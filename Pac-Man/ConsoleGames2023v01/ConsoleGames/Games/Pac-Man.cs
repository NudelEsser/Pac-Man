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
            return new Score();
        }
    }
}
