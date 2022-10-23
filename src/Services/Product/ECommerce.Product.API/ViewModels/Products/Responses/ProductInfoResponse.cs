using System.Text.Json.Serialization;

namespace ECommerce.Products.API.ViewModels.Products.Responses
{
    public class ProductInfoResponse
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public Guid Code { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}