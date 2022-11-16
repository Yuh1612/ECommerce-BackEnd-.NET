using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Shared.Infrastructure;
using ECommerce.Shared.Interfaces;
using ECommerce.Shared.Services;
using ELDesk.Shared.Infrastructure.Service.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Reflection;

namespace ECommerce.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddImplementationsAsInterfaces(this IServiceCollection services
            , Type interfaceType
            , params Type[] implementationAssemblyTypes)
        {
            foreach (var assemblyType in implementationAssemblyTypes)
            {
                var assembly = Assembly.GetAssembly(assemblyType);
                if (assembly == null) continue;

                var implementationTypes = assembly
                    .GetTypes()
                    .Where(_ =>
                        _.IsClass && !_.IsAbstract && !_.IsInterface
                        && _.GetInterface(interfaceType.Name) != null
                        && !_.IsGenericType
                    );

                foreach (var implementationType in implementationTypes)
                {
                    var mainInterfaces = implementationType
                        .GetInterfaces()
                        .Where(_ => _.GenericTypeArguments.Length == 0);

                    foreach (var mainInterface in mainInterfaces)
                    {
                        services.AddScoped(mainInterface, implementationType);
                    }
                }
            }
        }

        public static void AddImplementationsAsBaseClass(this IServiceCollection services
            , Type baseClassType
            , params Type[] implementationAssemblyTypes)
        {
            foreach (var assemblyType in implementationAssemblyTypes)
            {
                var assembly = Assembly.GetAssembly(assemblyType);
                if (assembly == null) continue;

                var implementationTypes = assembly
                    .GetTypes()
                    .Where(_ =>
                        _.IsClass && !_.IsAbstract && !_.IsInterface
                        && baseClassType.IsAssignableFrom(_)
                    );

                foreach (var implementationType in implementationTypes)
                {
                    var mainInterfaces = implementationType
                        .GetInterfaces()
                        .Where(_ => _.GenericTypeArguments.Count() == 0);

                    services.AddScoped(implementationType);
                }
            }
        }

        public static void AddUnitOfWork<TContext>(this IServiceCollection services, bool isDefaultContext = true)
            where TContext : DbContext
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            if (isDefaultContext) services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<TContext>));
        }

        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                return httpContext.CurrentUser();
            });
        }
    }
}