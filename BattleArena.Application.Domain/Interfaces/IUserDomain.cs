using BattleArena.Core.PostgreSQL.Models;
using System;
using System.Linq;

namespace BattleArena.Application.Domain.Interfaces
{
    public interface IUserDomain
    {
        DbUser Add(DbUser dbUser);
        DbUser Get(Guid idUser);
        DbUser Update(DbUser dbUser);
        IQueryable<DbUser> GetAll();
        int GetNextUserNumber();
    }
}
