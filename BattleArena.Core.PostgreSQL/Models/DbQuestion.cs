using System;
using System.ComponentModel.DataAnnotations;

namespace BattleArena.Core.PostgreSQL.Models
{
    public class DbQuestion
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(5000)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string Answer { get; set; }
    }
}