using System.Linq.Expressions;

namespace Ecommerce.Utilities.EFCore.Interfaces
{
    public interface IEFCoreRepository<T> : IAsyncRepository<T>, ISyncRepository<T> where T : class
    {
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression);

        IQueryable<T> GetQuery();
    }
}