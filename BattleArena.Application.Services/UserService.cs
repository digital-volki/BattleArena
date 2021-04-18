using BattleArena.Application.Domain.Interfaces;
using BattleArena.Application.Domain.Mapping;
using BattleArena.Application.Domain.Models;
using BattleArena.Application.Services.Interfaces;
using BattleArena.Core.PostgreSQL.Models;
using System;
using System.Linq;

namespace BattleArena.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDomain _userDomain;
        public UserService(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }

        public User GenerateUser()
        {
            DbUser newUser = new DbUser
            {
                Id = Guid.NewGuid().ToString(),
                Energe = 10,
                UserName = $"User{_userDomain.GetNextUserNumber()}"
            };

            return Mapping.MapUser(_userDomain.Add(newUser));
        }
        public User GetUser(Guid idUser)
        {
            DbUser dbUser = _userDomain.Get(idUser);
            if (dbUser == null)
            {
                return null;
            }

            return Mapping.MapUser(dbUser);
        }
        public IQueryable<User> GetUsers(League league)
        {
            var leagueRange = league.GetExperienceRange();
            return _userDomain.GetAll().OrderByDescending(u => u.Experience).Where(u => u.Experience >= leagueRange.Item1 && u.Experience < leagueRange.Item2).Select(u => Mapping.MapUser(u));
        }
    }
}
