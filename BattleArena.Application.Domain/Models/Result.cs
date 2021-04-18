using System;

namespace BattleArena.Application.Domain.Models
{
    public class Result
    {
        public Guid Id { get; set; }
        public Guid BattleId { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public bool Res { get; set; }
    }
}