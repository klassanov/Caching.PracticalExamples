using Caching.DbDependency.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Caching.DbDependency.Repository.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
    }
}
