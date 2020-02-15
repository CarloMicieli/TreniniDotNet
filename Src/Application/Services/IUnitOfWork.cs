using System.Threading.Tasks;

namespace TreniniDotNet.Application.Services
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
