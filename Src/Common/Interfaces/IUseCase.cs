using System.Threading;
using System.Threading.Tasks;

namespace TreniniDotNet.Common.Interfaces
{
    public interface IUseCase<in TUseCaseInput>
        where TUseCaseInput : IUseCaseInput
    {
        Task Execute(TUseCaseInput input);
    }
}
