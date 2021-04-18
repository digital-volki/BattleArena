using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BattleArena.Core.PostgreSQL.Models
{
    public class IdentityDbContext : IdentityDbContext<DbUser>
    {
        public new DbSet<DbUser> Users { get; set; }
        public DbSet<DbBattle> Battles { get; set; }
        public DbSet<DbTask> Task { get; set; }

        public IdentityDbContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DbBattle>()
                .HasMany(b => b.Results)
                .WithOne(r => r.Battle);
        }
    }
}
