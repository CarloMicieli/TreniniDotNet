using System.Threading.Tasks;
using TreniniDotNet.Application.Services;

namespace TreniniDotNet.Application.InMemory
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task<int> SaveAsync()
        {
            return await Task.FromResult<int>(0);
        }
    }
}
