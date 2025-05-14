using Caching.Hybrid.Aspire.Shared;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);


var apiConfiguration = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();


var redis = builder.AddRedis(name: Constants.RedisConnectionStringName, port: 6372)
                  .WithImage("redis")
                  .WithImageTag("latest")
                  .WithContainerName("redis")
                  .WithRedisInsight(options =>
                  {
                      options.WithContainerName("redis-insight")
                             .WithHostPort(8001);
                  });

builder.AddProject<Projects.Caching_Hybrid_Aspire_API>("caching-demo-api", launchProfileName: "https")
       .WithReference(redis)
       .WaitFor(redis)
       .WithReplicas(apiConfiguration!.NumReplicas);

builder.Build().Run();
