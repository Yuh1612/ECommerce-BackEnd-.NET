using Ecommerce.Utilities.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Shared.Interfaces
{
    public interface IUnitOfWork : IEFCoreUnitOfWork
    {
    }

    public interface IUnitOfWork<T> : IUnitOfWork, IEFCoreUnitOfWork<T> where T : DbContext
    {
    }
}