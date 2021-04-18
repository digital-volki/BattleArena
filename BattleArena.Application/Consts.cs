using System;
using System.Collections.Generic;

namespace BattleArena.Application
{
    public static class Consts
    {
        public static readonly Dictionary<League, int> LeagueExperience = new Dictionary<League, int>
        {
            { League.Transistor, 0 },
            { League.CPU, 7000 },
            { League.Quantum, 27000 }
        };

        public static readonly Dictionary<League, Tuple<int, int>> Awards = new Dictionary<League, Tuple<int, int>>
        {
            { League.Transistor, new Tuple<int, int>(300, 300) },
            { League.CPU, new Tuple<int, int>(600, 600) },
            { League.Quantum, new Tuple<int, int>(1000, 1000) }
        };

        public static double LoserPercent => 0.25;

        public static TimeSpan EnergeСooldown => TimeSpan.FromMinutes(5);
    }
}
