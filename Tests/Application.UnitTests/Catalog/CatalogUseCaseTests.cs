using System;
using TreniniDotNet.Application.Catalog.Brands;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.Catalog.Railways;
using TreniniDotNet.Application.Catalog.Scales;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.TestHelpers.InMemory.Services;

namespace TreniniDotNet.Application.Catalog
{
    public abstract class CatalogUseCaseTests<TUseCase, TUseCaseOutput, TOutputPort>
        : UseCaseTestHelper<TUseCase, TUseCaseOutput, TOutputPort>
            where TUseCaseOutput : IUseCaseOutput
            where TOutputPort : IOutputPortStandard<TUseCaseOutput>, new()
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeBrandsUseCase(Start initData, Func<BrandService, TOutputPort, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);

            var brandRepository = new BrandRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var brandService = new BrandService(brandRepository, new BrandsFactory(_fakeClock, _guidSource));
            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                useCaseBuilder(brandService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }

        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeRailwaysUseCase(Start initData, Func<RailwayService, TOutputPort, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);
            var railwayRepository = new RailwayRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var railwayService = new RailwayService(railwayRepository, new RailwaysFactory(_fakeClock, _guidSource));
            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                useCaseBuilder(railwayService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }

        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeScalesUseCase(Start initData, Func<ScaleService, TOutputPort, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);

            var scalesFactory = new ScalesFactory(_fakeClock, _guidSource);
            var scaleRepository = new ScaleRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var scaleService = new ScaleService(scaleRepository, scalesFactory);
            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                useCaseBuilder(scaleService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }

        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeCatalogItemUseCase(Start initData, Func<CatalogItemService, TOutputPort, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);

            var catalogItemsFactory = new CatalogItemsFactory(_fakeClock, _guidSource);
            var catalogItemRepository = new CatalogItemRepository(context);
            var brandRepository = new BrandRepository(context);
            var scaleRepository = new ScaleRepository(context);
            var railwayRepository = new RailwayRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var catalogItemService = new CatalogItemService(
                catalogItemRepository,
                catalogItemsFactory,
                brandRepository,
                scaleRepository,
                railwayRepository);
            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                useCaseBuilder(catalogItemService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }
    }
}
