using Microsoft.EntityFrameworkCore;
using System;

namespace BattleArena.Core.PostgreSQL.Models
{
    [Index("Battle", "User", "Question", IsUnique = true)]
    public class DbResult
    {
        public Guid Id { get; set; }
        public DbBattle Battle { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public bool Result { get; set; }
    }
}
