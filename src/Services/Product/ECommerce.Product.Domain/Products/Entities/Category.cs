#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; } = new HashSet<ProductCategories>();
    }
}