using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public sealed class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly IConnectionProvider _connectionProvider;
        
        public InMemoryUnitOfWork(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
        }
        
        public Task<int> SaveAsync()
        {
            return Task.FromResult(0);
        }

        public async Task<int> ExecuteAsync(string cmd, object param)
        {
            await using var connection = _connectionProvider.Create();
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(cmd, param);
            return result;
        }

        public async Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param)
        {
            await using var connection = _connectionProvider.Create();
            await connection.OpenAsync();
            var result = await connection.ExecuteScalarAsync<TResult>(sql, param);
            return result;            
        }

        public async Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param)
        {
            await using var connection = _connectionProvider.Create();
            await connection.OpenAsync();
            var result = await connection.QueryAsync<TResult>(sql, param);
            return result;    
        }

        public async Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param) where TResult : class
        {
            await using var connection = _connectionProvider.Create();
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<TResult>(sql, param);
            return result;    
        }
    }
}