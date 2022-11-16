#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Search
    {
        public Guid UserId { get; set; }
        public string Keyword { get; set; }
    }
}