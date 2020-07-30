using System.Threading.Tasks;

namespace TreniniDotNet.Common.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
    }
}
