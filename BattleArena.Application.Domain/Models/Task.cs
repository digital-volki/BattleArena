using System;
using System.Collections.Generic;

namespace BattleArena.Application.Domain.Models
{
    public class Task
    {
        public Guid TaskId { get; set; }
        public List<Question> Questions { get; set; }
    }
}