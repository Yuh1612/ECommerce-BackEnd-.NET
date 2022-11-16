using RabbitMQ.Events;

namespace ECommerce.Products.RabbitMQ.IntegrationEvents
{
    public class CreateProductEvent : IntegrationEvent
    {
        public CreateProductEvent()
        {
        }

        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int Weight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public double? Discount { get; set; }
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}