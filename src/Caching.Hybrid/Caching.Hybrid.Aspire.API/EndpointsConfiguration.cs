using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace Caching.Hybrid.Aspire.API
{
    public static class EndpointsConfiguration
    {
        private const string productsKey = "products";

        public static WebApplication MapMinimalAPIs(this WebApplication app)
        {
            app.MapGet("/", () => "Hello world!");

            app.MapGet("/products", async (HybridCache cache) =>
            {
                var products =
                await cache.GetOrCreateAsync(
                    key: productsKey,
                    factory: ProductsFactory);

                return Results.Ok(products);
            });

            app.MapGet("/localcache", async (IMemoryCache memoryCache) =>
            {
                var localCache = memoryCache as MemoryCache;
                return Results.Ok(value: localCache!.Keys);
            });

            app.MapPost("/invalidate/products", async (HybridCache cache, [FromServices] IConnectionMultiplexer multiplexer) =>
            {
                await cache.RemoveAsync(key: productsKey);
                await PublishCacheInvalidationMessage(multiplexer);
            });

            return app;
        }

        private static ValueTask<List<string>> ProductsFactory(CancellationToken cancellationToken)
            => ValueTask.FromResult<List<string>>(["product-1", "product-2", "product-3"]);

        private static async Task PublishCacheInvalidationMessage(IConnectionMultiplexer multiplexer)
        {
            var subscriber = multiplexer.GetSubscriber();
            await subscriber.PublishAsync(RedisChannel.Literal(Constants.CacheInvalidationChannel), new RedisValue(productsKey));
        }

    }
}
