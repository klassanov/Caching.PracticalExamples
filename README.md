# Caching.Demo

This repo contains 3 practical examples regarding caching. Each example is placed in a separate folder and in a separate solution. 
Useful `docker` commands as well as `Postgres` scripts needed for the examples are available in the [Commands.txt](tools/Commands.txt) file.



## 1. Hybrid Cache Example

[Caching.Hybrid Example Folder](src/Caching.Hybrid/)

Implementation Details:

* .NET 9
* Central Package Management
* Hybrid Cache: L1 Local In Memory Cache + L2 Distributed Redis Cache
* .NET Aspire Project using Docker containers
* .NET Minimal APIs



## 2. Database Dependency Caching Example

[Caching.DbDependency Example Folder](src/Caching.DbDependency/)

Implementation Details:

* .NET 9
* Central Package Management
* In-Memory Cache
* Postgres DB in Docker container
* Entity Framework Core
* Razor Pages



## 3. File Dependency Caching Example

[Caching.FileDependency Example Folder](src/Caching.FileDependency/)

Implementation Details:

* .NET 9
* In-Memory Cache
* Razor Pages
