using System;

namespace BattleArena.Application.Domain.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
    }
}
