using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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