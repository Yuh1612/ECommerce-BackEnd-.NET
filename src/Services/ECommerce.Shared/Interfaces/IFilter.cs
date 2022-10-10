using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Shared.Interfaces
{
    public interface IFilter<TEntity>
    {
        Expression<Func<TEntity, bool>> GetFilter();
    }
}