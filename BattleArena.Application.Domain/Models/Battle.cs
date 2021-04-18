using BattleArena.Core.PostgreSQL.Models.Enums;
using System;
using System.Collections.Generic;

namespace BattleArena.Application.Domain.Models
{
    public class Battle
    {
        public Guid BattleId { get; set; }
        public User Caller { get; set; }
        public User Receiver { get; set; }
        public List<Result> Results { get; set; }
        public Task Task { get; set; }
        public BattleStatus Status { get; set; }
        public DateTime EndDate { get; set; }
    }
}