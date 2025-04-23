using Caching.DbDependency.Repository.Context;
using Caching.DbDependency.Repository.Interfaces;
using Caching.DbDependency.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Caching.DbDependency.Repository
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("postgres-container"));
            });

            services.AddScoped<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
