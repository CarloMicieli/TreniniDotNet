using System;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.Railways;
using TreniniDotNet.Application.Catalog.Scales;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public abstract class CatalogItemUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort> : AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeUseCase(
            Start initData,
            Func<TOutputPort, CatalogItemsService, RollingStocksFactory, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);
            var rollingStocksFactory = new RollingStocksFactory(GuidSource);

            var catalogItemsService = new CatalogItemsService(
                new CatalogItemsFactory(Clock, GuidSource),
                new CatalogItemsRepository(context),
                new BrandsRepository(context),
                new RailwaysRepository(context),
                new ScalesRepository(context));

            return BuildUseCaseFixture(
                useCaseBuilder(OutputPort, catalogItemsService, rollingStocksFactory, UnitOfWork),
                context);
        }
    }
}
