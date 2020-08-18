using TreniniDotNet.Common.Data;
using TreniniDotNet.TestHelpers.InMemory.Repository;
using TreniniDotNet.TestHelpers.InMemory.Services;

namespace TreniniDotNet.Application.UseCases
{
    public sealed class UseCaseFixture<TUseCase, TOutputPort>
    {
        public UseCaseFixture(TUseCase useCase, TOutputPort outputPort, IUnitOfWork unitOfWork, InMemoryContext dbContext)
        {
            UseCase = useCase;
            OutputPort = outputPort;
            UnitOfWork = unitOfWork;
            DbContext = dbContext;
        }

        public void Deconstruct(out TUseCase useCase, out TOutputPort outputPort, out UnitOfWork unitOfWork, out InMemoryContext dbContext)
        {
            useCase = UseCase;
            outputPort = OutputPort;
            unitOfWork = UnitOfWork as UnitOfWork;
            dbContext = DbContext;
        }

        public void Deconstruct(out TUseCase useCase, out TOutputPort outputPort, out UnitOfWork unitOfWork)
        {
            useCase = UseCase;
            outputPort = OutputPort;
            unitOfWork = UnitOfWork as UnitOfWork;
        }

        public void Deconstruct(out TUseCase useCase, out TOutputPort outputPort)
        {
            useCase = UseCase;
            outputPort = OutputPort;
        }

        public TUseCase UseCase { get; }
        public TOutputPort OutputPort { get; }
        public IUnitOfWork UnitOfWork { get; }
        public InMemoryContext DbContext { get; }
    }
}
