using BattleArena.Core.PostgreSQL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BattleArena.Core.PostgreSQL.Models
{
    public class DbBattle
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid Caller { get; set; }
        [Required]
        public Guid Receiver { get; set; }
        [Required]
        public DbTask Task { get; set; }
        [Required]
        public BattleStatus Status { get; set; }
        public List<DbResult> Results { get; set; } = new List<DbResult>();
        public DateTime EndDate { get; set; }
    }
}
