using Ecommerce.Utilities.EFCore.Extensions;
using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Infrastructure.DbContexts;
using ECommerce.Shared.Extensions;
using MediatR;
using RabbitMQ;
using RabbitMQ.Models;
using System.Reflection;

namespace ECommerce.Products.RabbitMQ
{
    public class Program
    {
        private static ILogger<Program>? _logger;

        public static async Task Main(string[] args)
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<Program>();

            _logger?.LogInformation("Starting server RabbitMQ consumer!");
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            try
            {
                _logger?.LogInformation("Starting up the service");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"There was a problem starting the serivce: {ex.Message}");
                Console.ReadLine();
            }
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            _logger?.LogError(ex, $"Unhandled exception: {ex?.Message}");
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetRequiredSection("ConnectionString").Value!;
                    hostContext.Configuration.GetRequiredSection("RabbitMQSetting")
                        .Get<RabbitMQServiceBusSettings>(options => options.BindNonPublicProperties = true);

                    services.AddHostedService<Worker>();

                    services.AddHttpContextAccessor();

                    services.AddDbContext<ProductContext>(connectionString);
                    services.AddDbFactory<ProductContext>();
                    services.AddUnitOfWork<ProductContext>();

                    // Add Repositories
                    services.AddImplementationsAsInterfaces(typeof(IEFCoreRepository<>), typeof(ProductContext));

                    services.AddUserInfo();

                    services.AddRabbitMQServiceBus();
                    services.AddEventConsumptionService();
                    services.SubscribeIntegrationEvents(typeof(Program));

                    services.AddMediatR(Assembly.GetExecutingAssembly());
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Trace);
                });
        }
    }
}