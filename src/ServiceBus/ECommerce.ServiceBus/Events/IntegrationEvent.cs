using MediatR;
using System.Text.Json.Serialization;

namespace RabbitMQ.Events
{
    public abstract class IntegrationEvent : INotification
    {
        [JsonPropertyName("Id")]
        public Guid EventId { get; set; }

        public DateTime CreationDate { get; set; }

        public EventAction Action { get; set; }

        public string? GetEventName() => GetType().FullName;

        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(EventAction action) : this()
        {
            Action = action;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            EventId = id;
            CreationDate = createDate;
        }
    }
}