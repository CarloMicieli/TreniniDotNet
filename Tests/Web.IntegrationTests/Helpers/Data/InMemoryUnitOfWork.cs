using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbConnection _connection;
        
        public InMemoryUnitOfWork(IConnectionProvider connectionProvider)
        {
            _connection = connectionProvider.Create();
            _connection.Open();
        }
        
        public Task<int> SaveAsync()
        {
            return Task.FromResult(0);
        }

        public Task<int> ExecuteAsync(string cmd, object param) =>
            _connection.ExecuteAsync(cmd, param);

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param) =>
            _connection.ExecuteScalarAsync<TResult>(sql, param);

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param) =>
            _connection.QueryAsync<TResult>(sql, param);

        public Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param) where TResult : class =>
            _connection.QueryFirstOrDefaultAsync<TResult>(sql, param);

        public void Dispose()
        {
            _connection.Close();
        }
    }
}