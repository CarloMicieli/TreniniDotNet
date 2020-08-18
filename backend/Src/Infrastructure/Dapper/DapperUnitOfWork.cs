using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public sealed class DapperUnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        private readonly IConnectionProvider _connectionProvider;
        private readonly SemaphoreSlim _semaphore;

        public DapperUnitOfWork(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
            _semaphore = new SemaphoreSlim(1);
        }

        public Task<int> ExecuteAsync(string cmd, object param) =>
            CreateOrGetConnection().ExecuteAsync(cmd, param, _transaction);

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param) =>
            CreateOrGetConnection().ExecuteScalarAsync<TResult>(sql, param, _transaction);

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param) =>
            CreateOrGetConnection().QueryAsync<TResult>(sql, param, _transaction);

        public Task<TResult?> QueryFirstOrDefaultAsync<TResult>(string sql, object param)
            where TResult : class =>
            CreateOrGetConnection().QueryFirstOrDefaultAsync<TResult?>(sql, param, _transaction);

        private IDbConnection CreateOrGetConnection()
        {
            _semaphore.Wait();

            if (_connection is null)
            {
                _connection = _connectionProvider.Create();
                _connection.Open();

                _transaction = _connection.BeginTransaction();
            }

            _semaphore.Release();

            return _connection;
        }

        public Task<int> SaveAsync()
        {
            try
            {
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;

                _connection?.Dispose();
                _connection = null;
            }

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
