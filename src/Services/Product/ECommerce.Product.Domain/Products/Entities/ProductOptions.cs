#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class ProductOptions
    {
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }
        public string Description { get; set; }

        public virtual Option Option { get; set; }
        public virtual Product Product { get; set; }
    }
}