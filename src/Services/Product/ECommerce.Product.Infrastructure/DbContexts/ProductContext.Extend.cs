#nullable disable

using Microsoft.EntityFrameworkCore;

namespace ECommerce.Products.Infrastructure.DbContexts
{
    public partial class ProductContext : DbContext
    {
        private partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}