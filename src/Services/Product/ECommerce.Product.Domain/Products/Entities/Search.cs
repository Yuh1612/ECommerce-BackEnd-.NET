#nullable disable

namespace ECommerce.Products.Domain.Entities
{
    public partial class Search
    {
        public Guid UserId { get; set; }
        public string Keyword { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? CreateAt { get; set; }
        public Guid? CreateBy { get; set; }
    }
}