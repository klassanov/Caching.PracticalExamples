
using Caching.DbDependency.Repository.Entities;

namespace Caching.DbDependency.Web.GetProductsFeature
{
    public interface IProductsManager
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
