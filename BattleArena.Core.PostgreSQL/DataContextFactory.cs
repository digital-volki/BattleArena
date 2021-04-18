﻿using BattleArena.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BattleArena.Core.PostgreSQL
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(AppConfiguration.DatabaseFactoryConnection);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
