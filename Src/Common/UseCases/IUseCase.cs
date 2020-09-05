using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Common.UseCases
{
    public interface IUseCase<in TUseCaseInput> //, in TUseCaseOutput, in TUseCaseOutputPort>
        where TUseCaseInput : IUseCaseInput
        //where TUseCaseOutput : IUseCaseOutput
        //where TUseCaseOutputPort : IStandardOutputPort<TUseCaseOutput>
    {
        Task Execute(TUseCaseInput input);
    }
}
