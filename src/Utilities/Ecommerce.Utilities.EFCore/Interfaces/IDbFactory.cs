using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Utilities.EFCore.Interfaces
{
    public interface IDbFactory
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }

    public interface IDbFactory<T> : IDbFactory where T : DbContext
    {
        T DbContext { get; }
    }
}