using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BattleArena.Core.PostgreSQL.Models
{
    public class DbUser : IdentityUser
    {
        public int Experience { get; set; }
        public int Balance { get; set; }
        public int Energe { get; set; }
        public List<DbBattle> Battles { get; set; } = new List<DbBattle>();
    }
}
