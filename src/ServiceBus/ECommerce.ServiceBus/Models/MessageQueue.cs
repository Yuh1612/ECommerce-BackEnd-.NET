using System.Text.Json.Serialization;

namespace RabbitMQ.Models
{
    public class MessageQueue
    {
        public MessageQueue()
        {
        }

        public MessageQueue(string? body)
        {
            Content = body;
            CreatedOn = DateTime.UtcNow;
        }

        [JsonPropertyName("message")]
        public string? Content { get; set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; set; }

        [JsonIgnore]
        public string? QueueName { get; set; }
    }
}