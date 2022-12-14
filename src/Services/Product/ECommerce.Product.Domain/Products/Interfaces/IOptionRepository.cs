using Ecommerce.Utilities.EFCore.Interfaces;
using ECommerce.Products.Domain.Entities;

namespace ECommerce.Products.Domain.Products.Interfaces
{
    public interface IOptionRepository : IEFCoreRepository<Option>
    {
        Task<bool> AnyAsync(Guid id);
    }
}