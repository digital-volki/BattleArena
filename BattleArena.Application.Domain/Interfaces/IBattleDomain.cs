using BattleArena.Core.PostgreSQL.Models;
using System;

namespace BattleArena.Application.Domain.Interfaces
{
    public interface IBattleDomain
    {
        DbBattle Get(Guid battleId);
        DbBattle Create(DbBattle dbBattle);
        DbBattle Update(DbBattle dbBattle);
    }
}
