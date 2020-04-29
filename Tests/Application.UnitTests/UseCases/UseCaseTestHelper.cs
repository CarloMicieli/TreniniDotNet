using System;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class UseCaseTestHelper<TUseCase, TUseCaseOutput, TOutputPort>
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IOutputPortStandard<TUseCaseOutput>, new()
    {
        protected readonly IClock _fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));
        protected readonly IGuidSource _guidSource = FakeGuidSource.NewSource(new Guid("4a12d7b3-0e6b-4eee-8583-5b2da24c6fe3"));

        protected InMemoryContext NewMemoryContext(Start initData) =>
            initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();

        protected void SetNextGeneratedGuid(Guid id)
        {
            ((FakeGuidSource)_guidSource).FakeGuid = id;
        }
    }

    public enum Start
    {
        WithSeedData,
        Empty
    }
}
