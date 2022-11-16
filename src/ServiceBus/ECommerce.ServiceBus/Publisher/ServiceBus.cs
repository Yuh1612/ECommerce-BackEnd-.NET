using RabbitMQ.Client;
using RabbitMQ.Events;
using RabbitMQ.Models;
using System.Text;
using System.Text.Json;

namespace RabbitMQ.Publisher
{
    public class ServiceBus : IServiceBus
    {
        private RabbitMQConnection _connection;

        public ServiceBus(RabbitMQConnection connection)
        {
            _connection = connection;
        }

        public Task PublishEventAsync(IntegrationEvent @event)
        {
            var content = JsonSerializer.Serialize(@event
                , @event.GetType()
                , new JsonSerializerOptions {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                });

            var queue = new MessageQueue(content);

            var body = JsonSerializer.Serialize(queue);

            var bodyBin = Encoding.UTF8.GetBytes(body);

            var properties = _connection.Channel.CreateBasicProperties();
            properties.Persistent = true;

            _connection.Channel.BasicPublish(exchange: RabbitMQServiceBusSettings.Exchange
                , routingKey: @event.GetType().Name
                , basicProperties: properties
                , body: bodyBin);

            return Task.CompletedTask;
        }
    }
}