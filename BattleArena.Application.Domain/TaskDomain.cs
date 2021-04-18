using BattleArena.Application.Domain.Interfaces;
using BattleArena.Core.PostgreSQL;
using BattleArena.Core.PostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BattleArena.Application.Domain
{
    public class TaskDomain : ITaskDomain
    {
        private readonly IDataContext _dataContext;
        public TaskDomain(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public DbTask GetRandom()
        {
            DbTask dbTask = _dataContext.SqlQuery<DbTask>("SELECT * FROM public.\"Task\" ORDER BY RANDOM() LIMIT 1").FirstOrDefault();

            if (dbTask == null)
            {
                return null;
            }

            return _dataContext.GetQueryable<DbTask>()
                .Include(t => t.Questions)
                .FirstOrDefault(t => t.Id == dbTask.Id);
        }

        public List<DbResult> AddResult(List<DbResult> dbResults)
        {
            if (dbResults == null || !dbResults.Any())
            {
                return null;
            }

            List<DbResult> insertDbResults = _dataContext.InsertMany(dbResults).ToList();

            if (insertDbResults == null || !insertDbResults.Any())
            {
                return null;
            }

            if (_dataContext.Save() == 0)
            {
                return null;
            }

            return insertDbResults;
        }
    }
}
