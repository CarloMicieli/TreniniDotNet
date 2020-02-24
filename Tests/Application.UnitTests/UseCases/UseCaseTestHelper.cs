using System;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Application.InMemory.Repositories;
using TreniniDotNet.Application.InMemory.Repositories.Catalog;
using TreniniDotNet.Application.InMemory.Services;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class UseCaseTestHelper<TUseCase, TUseCaseOutput, TOutputPort>
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort: IOutputPortStandard<TUseCaseOutput>, new()
    {
        protected (TUseCase, TOutputPort) ArrangeBrandsUseCase(Start initData, Func<BrandService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithCatalogSeedData() : new InMemoryContext();
            var brandRepository = new BrandRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var brandService = new BrandService(brandRepository);
            var outputPort = new TOutputPort();

            return (factory.Invoke(brandService, outputPort, unitOfWork), outputPort);
        }

        protected (TUseCase, TOutputPort) ArrangeRailwaysUseCase(Start initData, Func<RailwayService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithCatalogSeedData() : new InMemoryContext();
            var railwayRepository = new RailwayRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var railwayService = new RailwayService(railwayRepository);
            var outputPort = new TOutputPort();

            return (factory.Invoke(railwayService, outputPort, unitOfWork), outputPort);
        }

        protected (TUseCase, TOutputPort) ArrangeScalesUseCase(Start initData, Func<ScaleService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = initData == Start.WithSeedData ? InMemoryContext.WithCatalogSeedData() : new InMemoryContext();
            var scaleRepository = new ScaleRepository(context);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var scaleService = new ScaleService(scaleRepository);
            var outputPort = new TOutputPort();

            return (factory.Invoke(scaleService, outputPort, unitOfWork), outputPort);
        }
    }

    public enum Start
    {
        WithSeedData,
        Empty
    }
}
