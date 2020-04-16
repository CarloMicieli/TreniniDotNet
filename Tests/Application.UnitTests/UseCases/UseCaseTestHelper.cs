using System;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Application.InMemory.Repositories;
using TreniniDotNet.Application.InMemory.Repositories.Catalog;
using TreniniDotNet.Application.InMemory.Repositories.Collection;
using TreniniDotNet.Application.InMemory.Services;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Common.Uuid.Testing;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class UseCaseTestHelper<TUseCase, TUseCaseOutput, TOutputPort>
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IOutputPortStandard<TUseCaseOutput>, new()
    {
        private readonly IClock _fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));
        private readonly IGuidSource _guidSource = FakeGuidSource.NewSource(new Guid("4a12d7b3-0e6b-4eee-8583-5b2da24c6fe3"));

        protected void SetNextId(Guid id)
        {
            ((FakeGuidSource)_guidSource).FakeGuid = id;
        }

        protected (TUseCase, TOutputPort) ArrangeBrandsUseCase(Start initData, Func<BrandService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();
            var brandRepository = new BrandRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var brandService = new BrandService(brandRepository, new BrandsFactory(_fakeClock, _guidSource));
            var outputPort = new TOutputPort();

            return (factory.Invoke(brandService, outputPort, unitOfWork), outputPort);
        }

        protected (TUseCase, TOutputPort) ArrangeRailwaysUseCase(Start initData, Func<RailwayService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();
            var railwayRepository = new RailwayRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var railwayService = new RailwayService(railwayRepository, new RailwaysFactory(_fakeClock, _guidSource));
            var outputPort = new TOutputPort();

            return (factory.Invoke(railwayService, outputPort, unitOfWork), outputPort);
        }

        protected (TUseCase, TOutputPort) ArrangeScalesUseCase(Start initData, Func<ScaleService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var scalesFactory = new ScalesFactory(_fakeClock, _guidSource);
            var context = initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();
            var scaleRepository = new ScaleRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var scaleService = new ScaleService(scaleRepository, scalesFactory);
            var outputPort = new TOutputPort();

            return (factory.Invoke(scaleService, outputPort, unitOfWork), outputPort);
        }

        protected (TUseCase, TOutputPort) ArrangeCatalogItemUseCase(Start initData, Func<CatalogItemService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();
            var catalogItemRepository = new CatalogItemRepository(context);
            var brandRepository = new BrandRepository(context);
            var scaleRepository = new ScaleRepository(context);
            var railwayRepository = new RailwayRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var catalogItemService = new CatalogItemService(
                catalogItemRepository,
                brandRepository,
                scaleRepository,
                railwayRepository);
            var outputPort = new TOutputPort();

            return (factory.Invoke(catalogItemService, outputPort, unitOfWork), outputPort);
        }

        protected (TUseCase, TOutputPort) ArrangeCollectionsUseCase(Start initData, Func<CollectionsService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithSeedData() : new InMemoryContext();
            var collectionsRepository = new CollectionsRepository(context);
            var shopsRepository = new ShopsRepository(context);
            var collectionsFactory = new CollectionsFactory(_fakeClock, _guidSource);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var collectionsService = new CollectionsService(collectionsFactory, collectionsRepository, shopsRepository);

            var outputPort = new TOutputPort();

            return (factory.Invoke(collectionsService, outputPort, unitOfWork), outputPort);
        }
    }

    public enum Start
    {
        WithSeedData,
        Empty
    }
}
