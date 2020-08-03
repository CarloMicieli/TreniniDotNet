using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data;

namespace TreniniDotNet.Infrastructure.Dapper
{
    public sealed class DapperUnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        private IDatabaseContext DatabaseContext { get; }

        public DapperUnitOfWork(IDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
            _connection = OpenConnection();
        }

        public Task<int> ExecuteAsync(string cmd, object param)
        {
            _transaction ??= OpenConnection().BeginTransaction();
            return _connection.ExecuteAsync(cmd, param, _transaction);
        }

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param)
        {
            _transaction ??= OpenConnection().BeginTransaction();
            return _connection.ExecuteScalarAsync<TResult>(sql, param, _transaction);
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param)
        {
            _transaction ??= OpenConnection().BeginTransaction();
            return _connection.QueryAsync<TResult>(sql, param, _transaction);
        }

        public Task<TResult?> QueryFirstOrDefaultAsync<TResult>(string sql, object param)
            where TResult : class
        {
            _transaction ??= OpenConnection().BeginTransaction();
            return _connection.QueryFirstOrDefaultAsync<TResult?>(sql, param, _transaction);
        }

        private IDbConnection OpenConnection()
        {
            if (_connection is null)
            {
                _connection = DatabaseContext.NewConnection();
                _connection.Open();
            }

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
            _connection?.Dispose();
        }
    }
}
