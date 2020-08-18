using System;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales
{
    public abstract class ScaleUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort> : AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeUseCase(
            Start initData,
            Func<TOutputPort, ScalesService, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);

            var scalesService = new ScalesService(
                new ScalesRepository(context),
                new ScalesFactory(Clock, GuidSource));

            return BuildUseCaseFixture(
                useCaseBuilder(OutputPort, scalesService, UnitOfWork),
                context);
        }
    }
}
