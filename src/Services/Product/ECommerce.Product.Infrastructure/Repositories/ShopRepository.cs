using Ecommerce.Utilities.EFCore;
using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Domain.Entities;
using ECommerce.Products.Domain.Products.Interfaces;

namespace ECommerce.Products.Infrastructure.Repositories
{
    public class ShopRepository : EFCoreRepository<Shop>, IShopRepository
    {
        public ShopRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await AnyAsync(_ => _.Id == id);
        }
    }
}