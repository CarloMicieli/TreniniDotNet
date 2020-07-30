namespace TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports
{
    public interface IStandardOutputPort<in TUseCaseOutput> : IErrorOutputPort
        where TUseCaseOutput : IUseCaseOutput
    {
        void Standard(TUseCaseOutput output);
    }
}