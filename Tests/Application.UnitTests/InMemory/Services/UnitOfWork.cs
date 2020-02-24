using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using Xunit;

namespace TreniniDotNet.Application.InMemory.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _saved = false;

        public async Task<int> SaveAsync()
        {
            _saved = true;
            return await Task.FromResult<int>(0);
        }

        public void ShouldBeSaved()
        {
            Assert.True(_saved, "IUnitOfWork.SaveAsync was not called.");
        }
    }
}
