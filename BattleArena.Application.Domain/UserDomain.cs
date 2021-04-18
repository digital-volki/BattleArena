using BattleArena.Application.Domain.Interfaces;
using BattleArena.Core.PostgreSQL;
using BattleArena.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BattleArena.Application.Domain
{
    public class UserDomain : IUserDomain
    {
        private readonly IDataContext _dataContext;

        public UserDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbUser Add(DbUser dbUser)
        {
            if (dbUser == null)
            {
                return null;
            }

            var insertUser = _dataContext.Insert(dbUser);

            if (insertUser == null)
            {
                return null;
            }

            if (_dataContext.Save() == 0)
            {
                return null;
            }

            return insertUser;
        }

        public DbUser Get(Guid idUser)
        {
            return _dataContext.GetQueryable<DbUser>()
                .Include(u => u.Battles)
                .FirstOrDefault(u => u.Id == idUser.ToString());
        }

        public IQueryable<DbUser> GetAll()
        {
            return _dataContext.GetQueryable<DbUser>();
        }

        public int GetNextUserNumber()
        {
            return _dataContext.GetQueryable<DbUser>().Count() + 1;
        }

        public DbUser Update(DbUser dbUser)
        {
            if (dbUser == null)
            {
                return null;
            }

            DbUser updateDbUser = _dataContext.Update(dbUser);

            if (updateDbUser == null)
            {
                return null;
            }

            if (_dataContext.Save() == 0)
            {
                return null;
            }

            return updateDbUser;
        }
    }
}
