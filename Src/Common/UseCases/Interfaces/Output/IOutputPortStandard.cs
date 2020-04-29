namespace TreniniDotNet.Common.UseCases.Interfaces.Output
{
    public interface IOutputPortStandard<in TUseCaseOutput> : IOutputPortErrors
        where TUseCaseOutput : IUseCaseOutput
    {
        void Standard(TUseCaseOutput output);
    }
}
