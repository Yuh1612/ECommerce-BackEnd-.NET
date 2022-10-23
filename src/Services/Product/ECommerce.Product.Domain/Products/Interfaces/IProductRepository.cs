using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Domain.Entities;

namespace ECommerce.Products.Domain.Products.Interfaces
{
    public interface IProductRepository : IEFCoreRepository<Product>
    {
        Task<bool> AnyAsync(Guid id);
    }
}