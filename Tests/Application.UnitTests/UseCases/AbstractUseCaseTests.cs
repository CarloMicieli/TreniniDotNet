using System;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.InMemory.Repository;
using TreniniDotNet.TestHelpers.InMemory.Services;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected IClock Clock { get; }
        protected IGuidSource GuidSource { get; }
        protected TOutputPort OutputPort { get; }
        protected IUnitOfWork UnitOfWork { get; }

        protected AbstractUseCaseTests()
        {
            Clock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));
            GuidSource = FakeGuidSource.NewSource(new Guid("4a12d7b3-0e6b-4eee-8583-5b2da24c6fe3"));

            OutputPort = new TOutputPort();
            UnitOfWork = new UnitOfWork();
        }

        protected UseCaseFixture<TUseCase, TOutputPort> BuildUseCaseFixture(TUseCase useCase, InMemoryContext context) =>
            new UseCaseFixture<TUseCase, TOutputPort>(
                useCase,
                OutputPort,
                UnitOfWork,
                context);

        protected InMemoryContext NewMemoryContext(Start initData) =>
            initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();

        protected void SetNextGeneratedGuid(Guid id)
        {
            ((FakeGuidSource)GuidSource).FakeGuid = id;
        }
    }

    public enum Start
    {
        WithSeedData,
        Empty
    }
}
