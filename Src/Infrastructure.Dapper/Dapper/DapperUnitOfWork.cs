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
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public DapperUnitOfWork(IDatabaseContext databaseContext)
        {
            _connection = databaseContext.NewConnection();
            
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public Task<int> ExecuteAsync(string cmd, object param) => 
            _connection.ExecuteAsync(cmd, param, _transaction);

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param) =>
            _connection.ExecuteScalarAsync<TResult>(sql, param, _transaction);

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param) =>
            _connection.QueryAsync<TResult>(sql, param, _transaction);

        public Task<TResult?> QueryFirstOrDefaultAsync<TResult>(string sql, object param)
            where TResult : class =>
            _connection.QueryFirstOrDefaultAsync<TResult?>(sql, param, _transaction);
        
        public Task<int> SaveAsync()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _connection.Dispose();
        }
    }
}
