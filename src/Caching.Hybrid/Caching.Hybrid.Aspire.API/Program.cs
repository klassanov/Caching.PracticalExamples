using Caching.Hybrid.Aspire.API;
using Caching.Hybrid.Aspire.ServiceDefaults;
using Microsoft.Extensions.Caching.Hybrid;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionMultiplexer = CreateRedisConnectionMultiplexer(builder);
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnectionMultiplexer);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString(Constants.RedisConnectionStringName);
});

builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(60)
    };
});

builder.Services.AddHostedService<CacheInvalidatorBackgroundService>();

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



static IConnectionMultiplexer CreateRedisConnectionMultiplexer(WebApplicationBuilder builder)
{
    var redisConnectionString = builder.Configuration.GetConnectionString(Constants.RedisConnectionStringName);
    return ConnectionMultiplexer.Connect(redisConnectionString!);
}