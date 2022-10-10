#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public string Slug { get; set; }
        public double? Discount { get; set; }
        public Guid? BrandId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreateAt { get; set; }
        public Guid? CreateBy { get; set; }

        public virtual Brand Brand { get; set; }
    }
}