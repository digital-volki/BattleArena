using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleArena.Application
{
    public enum League
    {
        Transistor = 1,
        CPU = 2,
        Quantum = 3
    }

    public static class LeagueExtension
    {
        /// <summary>
        /// Преобразует опыт в лигу.
        /// <b>Неважно от какого значения вызывается!</b>
        /// </summary>
        /// <returns></returns>
        public static League Convert(this League value, int experience)
        {
            League league = League.Transistor;
            foreach (var leaguePare in Consts.LeagueExperience)
            {
                if (experience >= leaguePare.Value)
                {
                    league = leaguePare.Key;
                }
            }
            return league;
        }

        /// <summary>
        /// Returns Par1 min exp, and Par2 max exp
        /// </summary>
        /// <returns></returns>
        public static (int, int) GetExperienceRange(this League league)
        {
            var leagueValues = ((IEnumerable<int>)Enum.GetValues(typeof(League))).ToList();
            var min = Consts.LeagueExperience[league];
            var max = leagueValues.Contains((int)league + 1) ? Consts.LeagueExperience[league + 1] : int.MaxValue;

            return (min, max);
        }
    }
}
