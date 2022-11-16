#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Brand
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; } = new HashSet<Product>();
    }
}