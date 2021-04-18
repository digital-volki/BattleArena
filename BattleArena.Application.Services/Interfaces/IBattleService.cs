using BattleArena.Application.Domain.Models;
using System;
using System.Collections.Generic;

namespace BattleArena.Application.Services.Interfaces
{
    public interface IBattleService
    {
        Battle Get(Guid battleId);
        Battle Create(Guid userId, Guid opponentId);
        Battle Start(Guid battleId, Guid userId);
        Battle CheckAnswers(Guid battleId, Guid userId, Dictionary<Guid, string> questionsAnswers);
    }
}
