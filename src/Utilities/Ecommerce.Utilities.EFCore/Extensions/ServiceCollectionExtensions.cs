using Ecommerce.Utilities.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Utilities.EFCore.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext<TContext>(this IServiceCollection services, string connectionString)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
                {
                    options.UseLazyLoadingProxies();
                    options.UseSqlServer(connectionString);
                });
        }

        public static void AddDbFactory<TContext>(this IServiceCollection services, bool isDefaultContext = true)
            where TContext : DbContext
        {
            services.AddScoped(typeof(IDbFactory<>), typeof(DbFactory<>));
            if (isDefaultContext) services.AddScoped(typeof(IDbFactory), typeof(DbFactory<TContext>));
        }
    }
}