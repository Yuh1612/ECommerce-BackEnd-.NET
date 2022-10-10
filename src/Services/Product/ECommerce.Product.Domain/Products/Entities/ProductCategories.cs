#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class ProductCategories
    {
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreateAt { get; set; }
        public Guid? CreateBy { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}