using Ecommerce.Utilities.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.EFCore
{
    public class EFCoreRepository<T> : IEFCoreRepository<T> where T : class
    {
        private readonly IDbFactory _dbFactory;
        private DbSet<T>? _dbSet;

        protected DbSet<T> DbSet => _dbSet ??= _dbFactory.Set<T>();

        public EFCoreRepository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #region Async

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return DbSet.AnyAsync(expression);
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return GetQuery().ToListAsync();
        }

        public virtual Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return DbSet.FirstOrDefaultAsync(expression);
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            return GetQuery(expression).ToListAsync();
        }

        public virtual async Task InsertRangeAsync(IEnumerable<T> entities)
        {
            if (entities?.Any() == true) await DbSet.AddRangeAsync(entities);
        }

        public virtual Task RemoveAsync(T entity)
        {
            Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task InsertAsync(T entity)
        {
            return DbSet.AddAsync(entity).AsTask();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            if (entities?.Any() == true) await DbSet.AddRangeAsync(entities);
        }

        public virtual Task UpdateAsync(T entity)
        {
            Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            UpdateRange(entities);
            return Task.CompletedTask;
        }

        #endregion Async

        #region Sync

        public virtual bool Any(Expression<Func<T, bool>> expression)
        {
            return DbSet.Any(expression);
        }

        public virtual T? Get(Expression<Func<T, bool>> expression)
        {
            return DbSet.FirstOrDefault(expression);
        }

        public virtual List<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public virtual List<T> GetList(Expression<Func<T, bool>> expression)
        {
            return GetQuery(expression).ToList();
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<T> entities)
        {
            if (entities?.Any() == true) DbSet.AddRange(entities);
        }

        public virtual void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            if (entities?.Any() == true) DbSet.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            if (entities?.Any() == true) DbSet.UpdateRange(entities);
        }

        #endregion Sync

        public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return DbSet;
        }
    }
}