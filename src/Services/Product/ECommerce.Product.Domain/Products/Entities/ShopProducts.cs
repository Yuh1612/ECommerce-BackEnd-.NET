#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class ShopProducts
    {
        public Guid ShopId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Shop Shop { get; set; }
    }
}