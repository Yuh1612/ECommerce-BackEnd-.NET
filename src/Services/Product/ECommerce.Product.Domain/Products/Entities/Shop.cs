#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Shop
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Product { get; set; } = new HashSet<Product>();
    }
}