using System;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Catalog.Brands
{
    public abstract class BrandUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort> : AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeUseCase(
            Start initData,
            Func<TOutputPort, BrandsService, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);

            var brandService = new BrandsService(
                new BrandsRepository(context),
                new BrandsFactory(Clock, GuidSource));

            return BuildUseCaseFixture(
                useCaseBuilder(OutputPort, brandService, UnitOfWork),
                context);
        }
    }
}

