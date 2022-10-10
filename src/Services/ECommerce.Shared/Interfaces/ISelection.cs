using System.Linq.Expressions;

namespace ECommerce.Shared.Interfaces
{
    public interface ISelection<TSource, TDestination>
    {
        Expression<Func<TSource, TDestination>> GetSelection();
    }
}