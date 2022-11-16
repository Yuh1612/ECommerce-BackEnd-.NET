namespace RabbitMQ.Models
{
    public class RabbitMQServiceBusSettings
    {
        public static string Host { get; set; } = "localhost";

        public static int Port { get; set; }

        public static string? User { get; set; }

        public static string? Password { get; set; }

        public static string? VirtualHost { get; set; }

        public static string? Exchange { get; set; }

        public static MessageQueueSettings? Queue { get; set; }

        public static bool HasRabbitSettingValue() => Queue != null && !string.IsNullOrEmpty(Host);
    }
}