namespace RabbitMQ.Models
{
    public class MessageQueueConsumption
    {
        public MessageQueueConsumption()
        {
        }

        public MessageQueueConsumption(string eventName, string? queueName, string? content, DateTime createdOn)
        {
            QueueName = queueName;
            Content = content;
            EventName = eventName;
            CreatedOn = createdOn;
        }

        public string? EventName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? Content { get; set; }

        public string? QueueName { get; set; }

        public static MessageQueueConsumption Parse(MessageQueue message, string eventName)
        {
            return new MessageQueueConsumption(eventName, message.QueueName, message.Content, message.CreatedOn);
        }
    }
}