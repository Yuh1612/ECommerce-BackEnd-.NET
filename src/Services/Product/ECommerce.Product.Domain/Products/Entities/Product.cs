#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Product
    {
        public Guid Code { get; set; }
        public Guid ShopId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public string Slug { get; set; }
        public double? Discount { get; set; }
        public Guid? BrandId { get; set; }
        public bool IsActive { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; } = new HashSet<ProductCategories>();
        public virtual ICollection<ProductOptions> ProductOptions { get; set; } = new HashSet<ProductOptions>();
    }
}