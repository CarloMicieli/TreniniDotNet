using System;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public abstract class ShopUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort> : AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeUseCase(
            Start initData,
            Func<TOutputPort, ShopsService, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);

            var shopsService = new ShopsService(
                new ShopsFactory(Clock, GuidSource),
                new ShopsRepository(context));

            return BuildUseCaseFixture(
                useCaseBuilder(OutputPort, shopsService, UnitOfWork),
                context);
        }
    }
}
