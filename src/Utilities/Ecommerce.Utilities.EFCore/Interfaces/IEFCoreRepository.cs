using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.EFCore.Interfaces
{
    public interface IEFCoreRepository<T> : IAsyncRepository<T>, ISyncRepository<T> where T : class
    {
        IQueryable<T> GetQuery(Expression<Func<T, bool>> expression);

        IQueryable<T> GetQuery();
    }
}