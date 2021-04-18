using BattleArena.Application.Domain.Interfaces;
using BattleArena.Core.PostgreSQL;
using BattleArena.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleArena.Application.Domain
{
    public class BattleDomain : IBattleDomain
    {
        private readonly IDataContext _dataContext;

        public BattleDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbBattle Get(Guid battleId)
        {
            return _dataContext.GetQueryable<DbBattle>()
                .Include(b => b.Task)
                .Include(b => b.Task.Questions)
                .Include(b => b.Results)
                .FirstOrDefault(b => b.Id == battleId);
        }

        public DbBattle Create(DbBattle dbBattle)
        {
            if (dbBattle == null)
            {
                return null;
            }

            DbBattle insertDbBattle = _dataContext.Insert(dbBattle);

            if (insertDbBattle == null)
            {
                return null;
            }

            if (_dataContext.Save() == 0)
            {
                return null;
            }

            return insertDbBattle;
        }

        public DbBattle Update(DbBattle dbBattle)
        {
            if (dbBattle == null)
            {
                return null;
            }

            DbBattle updateDbBattle = _dataContext.Update(dbBattle);

            if (updateDbBattle == null)
            {
                return null;
            }

            if (_dataContext.Save() == 0)
            {
                return null;
            }

            return updateDbBattle;
        }
    }
}
