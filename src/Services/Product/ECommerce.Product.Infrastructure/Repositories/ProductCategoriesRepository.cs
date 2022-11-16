using Ecommerce.Utilities.EFCore;
using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;

namespace ECommerce.Products.Infrastructure.Repositories
{
    public class ProductCategoriesRepository : EFCoreRepository<ProductCategories>, IProductCategoriesRepository
    {
        public ProductCategoriesRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}