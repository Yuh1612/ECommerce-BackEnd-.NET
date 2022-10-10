using System.Linq.Expressions;

namespace ECommerce.Shared.Interfaces
{
    public interface IFilter<TEntity>
    {
        Expression<Func<TEntity, bool>> GetFilter();
    }
}