using Ecommerce.Utilities.EFCore.CommandGenerators;
using Ecommerce.Utilities.EFCore.Extensions;
using Ecommerce.Utilities.EFCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Utilities.EFCore
{
    public class EFCoreUnitOfWork<T> : IEFCoreUnitOfWork<T> where T : DbContext
    {
        public T DbContext => throw new NotImplementedException();

        public async Task<int> ExecuteCommandAsync(ISqlCommandBase dbCommand)
        {
            var result = await DbContext.Database.ExecuteSqlRawAsync(dbCommand.Sql, dbCommand.Parameters);
            dbCommand.LoadOutputParameters();
            return result;
        }

        public async Task<List<TEntity>> ExecuteQueryAsync<TEntity>(ISqlCommandBase dbCommand) where TEntity : new()
        {
            var connection = GetDbConnection(out bool isNewConnection);
            using var command = CreateCommand(connection);
            command.CommandText = dbCommand.Sql;
            command.CommandType = dbCommand.CommandType;
            command.Parameters.AddRange(dbCommand.Parameters);

            if (connection.State == ConnectionState.Closed) await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            dbCommand.LoadOutputParameters();

            if (isNewConnection) await connection.CloseAsync();

            return dt.ToList<TEntity>();
        }

        public async Task<TResultSet> ExecuteQueryMultiSetsAsync<TResultSet>(ISqlCommandBase dbCommand) where TResultSet : new()
        {
            var connection = GetDbConnection(out bool isNewConnection);
            using var command = CreateCommand(connection);
            // add query and parameters
            command.CommandText = dbCommand.Sql;
            command.CommandType = dbCommand.CommandType;
            command.Parameters.AddRange(dbCommand.Parameters);

            // parse result sets to a model
            var result = new TResultSet();
            var props = typeof(TResultSet)
                .GetProperties()
                .Where(p => p.CanWrite && typeof(ICollection).IsAssignableFrom(p.PropertyType))
                .ToList();

            if (connection.State == ConnectionState.Closed) await connection.OpenAsync();

            // read foreach result set
            using var reader = await command.ExecuteReaderAsync();
            foreach (var prop in props)
            {
                using var dt = new DataTable();
                dt.Load(reader);
                prop.SetValue(result, dt.ToList(prop.PropertyType));
            }
            dbCommand.LoadOutputParameters();

            if (isNewConnection) await connection.CloseAsync();

            return result;
        }

        public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (DbContext.Database.CurrentTransaction != null) return await func.Invoke();

            var strategy = DbContext.Database.CreateExecutionStrategy();
            var transResult = await strategy.ExecuteAsync(async () =>
            {
                using var trans = await DbContext.Database.BeginTransactionAsync(isolationLevel);
                try
                {
                    var result = await func.Invoke();
                    await trans.CommitAsync();
                    return result;
                }
                catch
                {
                    await trans.RollbackAsync();
                    throw;
                }
            });
            return transResult;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        private DbConnection GetDbConnection(out bool isNew)
        {
            isNew = default;
            DbConnection? connection = null;
            if (DbContext.Database.CurrentTransaction != null)
            {
                connection = DbContext.Database
                    .CurrentTransaction
                    .GetDbTransaction()
                    .Connection;
            }
            connection ??= DbContext.Database.GetDbConnection();

            if (connection == null)
            {
                var open = DbContext.Database.OpenConnectionAsync();
                open.Wait();
                isNew = true;
                connection = DbContext.Database.GetDbConnection();
            }

            return connection;
        }

        private DbCommand CreateCommand(DbConnection connection)
        {
            var command = connection.CreateCommand();
            var currentTrans = DbContext.Database.CurrentTransaction;
            if (currentTrans != null)
            {
                command.Transaction = currentTrans.GetDbTransaction();
            }

            return command;
        }
    }
}