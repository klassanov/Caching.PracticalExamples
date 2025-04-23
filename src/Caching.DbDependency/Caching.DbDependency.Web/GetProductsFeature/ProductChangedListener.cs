using Npgsql;

namespace Caching.DbDependency.Web.GetProductsFeature
{
    public class ProductChangedListener : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ProductChangedListener> logger;
        private readonly IProductsChangeTokenProvider changeTokenProvider;
        private const string channelName = "product_changed";

        public ProductChangedListener(IConfiguration configuration, ILogger<ProductChangedListener> logger, IProductsChangeTokenProvider changeTokenProvider)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.changeTokenProvider = changeTokenProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var conn = new NpgsqlConnection(configuration.GetConnectionString("postgres-container")))
            {
                await conn.OpenAsync(stoppingToken);

                conn.Notification += OnProductsChanged;

                using (var cmd = new NpgsqlCommand($"LISTEN {channelName}", conn))
                {
                    await cmd.ExecuteNonQueryAsync(stoppingToken);
                    logger.LogInformation("Listening for products changes...");
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    await conn.WaitAsync(stoppingToken);
                }
            }
        }

        private void OnProductsChanged(object? sender, NpgsqlNotificationEventArgs e)
        {
            logger.LogInformation("Products changed: {payload}", e.Payload);
            changeTokenProvider.SignalChange();
        }
    }
}
