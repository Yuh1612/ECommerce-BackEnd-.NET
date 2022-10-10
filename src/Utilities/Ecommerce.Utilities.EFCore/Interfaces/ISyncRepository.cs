using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.EFCore.Interfaces
{
    public interface ISyncRepository<T>
    {
        void Insert(T entity);

        void InsertRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        T? Get(Expression<Func<T, bool>> expression);

        List<T> GetList(Expression<Func<T, bool>> expression);

        List<T> GetAll();

        bool Any(Expression<Func<T, bool>> expression);
    }
}