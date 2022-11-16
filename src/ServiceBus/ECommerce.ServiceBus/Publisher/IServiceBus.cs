using RabbitMQ.Events;

namespace RabbitMQ.Publisher
{
    public interface IServiceBus
    {
        Task PublishEventAsync(IntegrationEvent @event);
    }
}