using System.Threading.Tasks;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Infrastracture.Dapper
{
    public sealed class DapperUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveAsync()
        {
            return Task.FromResult(0);
        }
    }
}
