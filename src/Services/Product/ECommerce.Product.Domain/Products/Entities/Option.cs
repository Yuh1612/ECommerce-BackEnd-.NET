#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Option
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}