using BattleArena.Core.PostgreSQL.Models;
using System.Collections.Generic;

namespace BattleArena.Application.Domain.Interfaces
{
    public interface ITaskDomain
    {
        DbTask GetRandom();
        List<DbResult> AddResult(List<DbResult> dbResults);
    }
}
