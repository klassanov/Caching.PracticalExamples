# Caching.Demo

This repo contains 3 practical examples regarding caching. Each example is placed in a separate folder and in a separate solution. 
Useful `docker` commands as well as `Postgres` scripts needed for the examples are available in the [Commands.txt](tools/Commands.txt) file.





## 1. Cache Database Dependency Example

[Caching.DbDependency Example Folder](src/Caching.DbDependency/)

Implementation Details:

* .NET 9
* Central Package Management
* In-Memory Cache
* Postgres DB in Docker container
* Entity Framework Core
* Razor Pages






## 2. Hybrid Cache Example

[Caching.Hybrid Example Folder](src/Caching.Hybrid/)

Implementation Details:

* .NET 9
* Central Package Management
* Hybrid Cache: L1 In Memory + L2 Redis
* .NET Aspire Project using Docker containers
* .NET Minimal APIs
