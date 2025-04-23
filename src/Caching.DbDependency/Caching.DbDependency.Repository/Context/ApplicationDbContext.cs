using Caching.DbDependency.Repository.Entities;
using Caching.DbDependency.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caching.DbDependency.Repository.Context
{
    public class ApplicationDbContext: DbContext, IApplicationContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Product> Products { get; set; }
    }
}
