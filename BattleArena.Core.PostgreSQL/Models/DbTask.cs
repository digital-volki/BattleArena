using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleArena.Core.PostgreSQL.Models
{
    public class DbTask
    {
        [Key]
        public Guid Id { get; set; }
        public List<DbQuestion> Questions { get; set; }
    }
}