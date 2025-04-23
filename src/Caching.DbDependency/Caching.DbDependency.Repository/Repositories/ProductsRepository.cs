using Caching.DbDependency.Repository.Entities;
using Caching.DbDependency.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Caching.DbDependency.Repository.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IApplicationContext context;
        public ProductsRepository(IApplicationContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products
                                .OrderBy(p=>p.Name)
                                .ToListAsync();
        }
    }
}