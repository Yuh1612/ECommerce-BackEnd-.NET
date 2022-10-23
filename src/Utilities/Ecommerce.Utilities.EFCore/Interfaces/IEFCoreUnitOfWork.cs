using Ecommerce.Utilities.EFCore.CommandGenerators;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Utilities.EFCore.Interfaces
{
    public interface IEFCoreUnitOfWork
    {
        Task<List<TEntity>> ExecuteQueryAsync<TEntity>(ISqlCommandBase dbCommand) where TEntity : new();

        Task<TResultSet> ExecuteQueryMultiSetsAsync<TResultSet>(ISqlCommandBase dbCommand) where TResultSet : new();

        Task<int> ExecuteCommandAsync(ISqlCommandBase dbCommand);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }

    public interface IEFCoreUnitOfWork<T> : IEFCoreUnitOfWork where T : DbContext
    {
        public T DbContext { get; }
    }
}