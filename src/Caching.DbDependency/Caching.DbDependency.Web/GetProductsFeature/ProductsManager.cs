using Caching.DbDependency.Repository.Entities;
using Caching.DbDependency.Repository.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.DbDependency.Web.GetProductsFeature
{
    public class ProductsManager : IProductsManager
    {
        private readonly IMemoryCache cache;
        private readonly IProductsRepository productsRepository;
        private readonly IProductsChangeTokenProvider changeTokenProvider;
        private readonly ILogger<ProductsManager> logger;
        private const string ProductsCacheKey = "ProductsCacheKey";

        public ProductsManager(
            IMemoryCache cache,
            IProductsRepository productsRepository,
            IProductsChangeTokenProvider changeTokenProvider,
            ILogger<ProductsManager> logger)
        {
            this.cache = cache;
            this.productsRepository = productsRepository;
            this.changeTokenProvider = changeTokenProvider;
            this.logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> products = [];

            if (!cache.TryGetValue(ProductsCacheKey, out products!))
            {
                logger.LogInformation("Cache miss for products. Data will be fetched from repository and cached.");

                products = await productsRepository.GetAllAsync();

                var changeToken = changeTokenProvider.GetChangeToken();

                cache.Set(ProductsCacheKey, products, new MemoryCacheEntryOptions()
                                            .AddExpirationToken(changeToken)
                                            .RegisterPostEvictionCallback(OnProductsCacheEntryRemoved));
                // Absolute Expiration can be set to make sure that this cache entry is not kept forever
                //.SetAbsoluteExpiration(TimeSpan.FromMinutes(5)); 
            }

            else
            {
                logger.LogInformation("Cache hit for products.");
            }

            return products;
        }

        private void OnProductsCacheEntryRemoved(object key, object? value, EvictionReason reason, object? state)
        {
            logger.LogInformation("Cache entry removed: {key}, reason: {reason}", key, reason);
        }
    }
}
