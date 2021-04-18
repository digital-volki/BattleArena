using BattleArena.Application.Domain.Models;
using System;
using System.Linq;

namespace BattleArena.Application.Services.Interfaces
{
    public interface IUserService
    {
        User GenerateUser();
        User GetUser(Guid idUser);
        IQueryable<User> GetUsers(League league);
    }
}
