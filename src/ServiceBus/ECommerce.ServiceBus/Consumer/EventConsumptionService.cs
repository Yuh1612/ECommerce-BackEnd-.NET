using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Events;
using RabbitMQ.Models;
using System.Text.Json;

namespace RabbitMQ.Consumer
{
    public class EventConsumptionService : IEventConsumptionService
    {
        private readonly IMediator _mediator;

        private readonly ILogger<EventConsumptionService> _logger;

        private readonly IntegrationEventCollection _eventCollection;

        public EventConsumptionService(IMediator mediator
            , ILogger<EventConsumptionService> logger
            , IntegrationEventCollection eventCollection)
        {
            _mediator = mediator;
            _logger = logger;
            _eventCollection = eventCollection;
        }

        public async Task<bool> ProcessAsync(MessageQueueConsumption message)
        {
            if (_eventCollection.ContainsKey(message.EventName!))
            {
                try
                {
                    var @event = JsonSerializer.Deserialize(message.Content!, _eventCollection[message.EventName!]);
                    await _mediator.Publish(@event!);

                    _logger.LogInformation("\n\nProcessed event: " + message.EventName);
                    _logger.LogInformation("Message: " + message.Content);

                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}, Trace: {ex.StackTrace}");

                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                }
            }

            return true;
        }
    }
}