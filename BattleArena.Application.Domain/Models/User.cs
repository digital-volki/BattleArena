using System;
using System.Collections.Generic;

namespace BattleArena.Application.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int Experience { get; set; }
        public League League => League.Transistor.Convert(Experience);
        public int Balance { get; set; }
        public int Energe { get; set; }
        public List<Battle> Battles { get; set; }
    }
}
