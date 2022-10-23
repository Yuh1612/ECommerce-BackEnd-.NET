#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Option
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductOptions> ProductOptions { get; set; } = new HashSet<ProductOptions>();
    }
}