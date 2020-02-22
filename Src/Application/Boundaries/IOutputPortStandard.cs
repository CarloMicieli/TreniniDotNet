namespace TreniniDotNet.Application.Boundaries
{
    public interface IOutputPortStandard<in TUseCaseOutput> : IOutputPortErrors
        where TUseCaseOutput : IUseCaseOutput
    {
        void Standard(TUseCaseOutput output);
    }
}
