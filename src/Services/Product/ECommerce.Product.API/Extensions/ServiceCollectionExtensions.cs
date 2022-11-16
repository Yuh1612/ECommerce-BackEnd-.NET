using Ecommerce.Utilities.EFCore.Extensions;
using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ECommerce.Shared.Indentity;
using ECommerce.Shared.Services;
using System.Reflection;
using MediatR;
using RabbitMQ.Models;
using RabbitMQ;

namespace ECommerce.Products.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetRequiredSection("ConnectionString").Value!;
            string secretKey = configuration.GetRequiredSection("SecretKey").Value!;
            configuration.GetRequiredSection("RabbitMQSetting")
                        .Get<RabbitMQServiceBusSettings>(options => options.BindNonPublicProperties = true);

            services.AddCors();

            services.AddHttpContextAccessor();

            services.AddRabbitMQServiceBus();

            services.AddDbContext<ProductContext>(connectionString);
            services.AddDbFactory<ProductContext>();
            services.AddUnitOfWork<ProductContext>();

            services.AddRepositories();

            services.AddAuthentication(secretKey);

            services.AddControllers();

            services.AddUserInfo();

            services.AddAppServices();

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddImplementationsAsInterfaces(typeof(IEFCoreRepository<>), typeof(ProductContext));
        }

        public static void AddAuthentication(this IServiceCollection services, string secretKey)
        {
            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        public static void AddControllers(this IServiceCollection services)
        {
            services.AddControllers(option =>
            {
                option.RespectBrowserAcceptHeader = true;
                option.Filters.Add<AllowAnonymousFilter>();
            });
        }

        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddImplementationsAsBaseClass(typeof(SharedService), typeof(Program));
        }
    }
}