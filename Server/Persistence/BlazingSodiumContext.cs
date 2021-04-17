using BlazingSodium.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazingSodium.Server.Persistence
{
    public class BlazingSodiumContext : DbContext
    {
        public BlazingSodiumContext()
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public BlazingSodiumContext(DbContextOptions<BlazingSodiumContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
