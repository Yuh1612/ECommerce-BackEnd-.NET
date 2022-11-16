using RabbitMQ.Events;
using System.Text.Json.Serialization;

namespace ECommerce.Products.RabbitMQ.IntegrationEvents
{
    public class RegisterShopEvent : IntegrationEvent
    {
        [JsonPropertyName("id")]
        public string? ShopId { get; set; }

        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        [JsonPropertyName("profile")]
        public Profile? Profile { get; set; }
    }

    public class Profile
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}