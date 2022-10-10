using Ecommerce.Utilities.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Utilities.EFCore
{
    public class DbFactory<T> : IDbFactory<T>, IDisposable where T : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _isDisposed;
        private T? _dbContext;

        public T DbContext => _dbContext ??= _serviceProvider.GetRequiredService<T>();

        public DbFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            if (!_isDisposed && _dbContext != null)
            {
                _isDisposed = true;
                DbContext.Dispose();
            }
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return DbContext.Set<TEntity>();
        }
    }
}