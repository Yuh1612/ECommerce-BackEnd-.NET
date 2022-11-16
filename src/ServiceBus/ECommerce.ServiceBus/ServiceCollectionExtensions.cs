using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Consumer;
using RabbitMQ.Events;
using RabbitMQ.Models;
using RabbitMQ.Publisher;

namespace RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitMQServiceBus(this IServiceCollection services)
        {
            if (!RabbitMQServiceBusSettings.HasRabbitSettingValue())
                throw new Exception("The RabbitMQServiceBusSettings class must be have full infomation, please fill in");

            services.AddSingleton(new RabbitMQConnection(
                    RabbitMQServiceBusSettings.Host
                    , RabbitMQServiceBusSettings.Port
                    , RabbitMQServiceBusSettings.User
                    , RabbitMQServiceBusSettings.Password
                    , RabbitMQServiceBusSettings.VirtualHost));

            services.AddSingleton<IServiceBus>((provider) =>
            {
                var connection = provider.GetRequiredService<RabbitMQConnection>();
                return new ServiceBus(connection);
            });
        }

        public static void AddEventConsumptionService(this IServiceCollection services)
        {
            services.AddScoped<IEventConsumptionService, EventConsumptionService>();
        }

        public static void SubscribeIntegrationEvents(this IServiceCollection services, params Type[] assemblyTypes)
        {
            var collection = new IntegrationEventCollection(assemblyTypes);
            services.AddSingleton(collection);
        }
    }
}