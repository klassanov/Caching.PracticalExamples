using Caching.Hybrid.Aspire.Shared;
using StackExchange.Redis;
using HybridCache = Microsoft.Extensions.Caching.Hybrid.HybridCache;

namespace Caching.Hybrid.Aspire.API
{
    public class CacheInvalidatorBackgroundService : BackgroundService
    {
        private HybridCache hybridCache; // Ensure HybridCache is a class, not a namespace
        private readonly IConnectionMultiplexer connectionMultiplexer;
        private readonly ILogger<CacheInvalidatorBackgroundService> logger;

        public CacheInvalidatorBackgroundService(HybridCache hybridCache, IConnectionMultiplexer connectionMultiplexer, ILogger<CacheInvalidatorBackgroundService> logger)
        {
            this.hybridCache = hybridCache;
            this.connectionMultiplexer = connectionMultiplexer;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = connectionMultiplexer.GetSubscriber();

            await subscriber.SubscribeAsync(RedisChannel.Literal(Constants.CacheInvalidationChannel), async (channel, cacheKey) =>
            {
                await hybridCache.RemoveAsync(cacheKey!);
                logger.LogInformation("Cache invalidation message received for key: {CacheKey}. Key Invalidated", cacheKey);
            });

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
