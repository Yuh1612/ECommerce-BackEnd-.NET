using RabbitMQ.Models;

namespace RabbitMQ.Consumer
{
    public interface IEventConsumptionService
    {
        Task<bool> ProcessAsync(MessageQueueConsumption message);
    }
}