﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.EFCore.Interfaces
{
    public interface IAsyncRepository<T>
    {
        Task InsertAsync(T entity);

        Task InsertRangeAsync(IEnumerable<T> entities);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(IEnumerable<T> entities);

        Task<T?> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllAsync();

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}