using System.Threading.Tasks;

namespace TreniniDotNet.Common.UseCases.Interfaces.Input
{
    public interface IUseCase<in TUseCaseInput>
        where TUseCaseInput : IUseCaseInput
    {
        Task Execute(TUseCaseInput input);
    }
}
