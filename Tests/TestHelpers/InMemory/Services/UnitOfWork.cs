using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data;

namespace TreniniDotNet.TestHelpers.InMemory.Services
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private bool _saved = false;

        public async Task<int> SaveAsync()
        {
            _saved = true;
            return await Task.FromResult<int>(0);
        }

        public Task<int> ExecuteAsync(string cmd, object param)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param) where TResult : class
        {
            throw new System.NotImplementedException();
        }

        public void EnsureUnitOfWorkWasSaved()
        {
            _saved.Should().BeTrue("IUnitOfWork.SaveAsync was not called.");
        }
    }
}
