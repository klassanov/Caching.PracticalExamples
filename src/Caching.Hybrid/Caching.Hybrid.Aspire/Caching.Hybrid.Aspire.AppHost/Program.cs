var builder = DistributedApplication.CreateBuilder(args);


var redis = builder.AddRedis(name: "redis", port:6372)
                   .WithImage("redis")
                   .WithImageTag("latest")
                   .WithContainerName("redis")
                   .WithRedisInsight(options =>
                   {
                       options.WithContainerName("redis-insight")
                              .WithHostPort(8001);
                   });

builder.AddProject<Projects.Caching_Hybrid_Aspire_API>("caching-demo-api")
       .WithReference(redis)
       .WithReplicas(2);


builder.Build().Run();
