using Caching.Hybrid.Aspire.API;
using Caching.Hybrid.Aspire.ServiceDefaults;
using Caching.Hybrid.Aspire.Shared;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

var apiConfiguration = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.AddRedisDistributedCache(Constants.RedisConnectionStringName);

builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(60)
    };
});

if (apiConfiguration!.RegisterCacheInvalidatorService)
{
    builder.Services.AddHostedService<CacheInvalidatorBackgroundService>();
}


builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapMinimalAPIs();

app.Run();
