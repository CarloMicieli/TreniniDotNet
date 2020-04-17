using System;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Application.InMemory.Repositories.Collection;
using TreniniDotNet.Application.InMemory.Services;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class CollectionUseCaseTests<TUseCase, TUseCaseOutput, TOutputPort>
        : UseCaseTestHelper<TUseCase, TUseCaseOutput, TOutputPort>
            where TUseCaseOutput : IUseCaseOutput
            where TOutputPort : IOutputPortStandard<TUseCaseOutput>, new()
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeCollectionsUseCase(
            Start initData,
            Func<CollectionsService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = NewMemoryContext(initData);

            var collectionsRepository = new CollectionsRepository(context);
            var shopsRepository = new ShopsRepository(context);
            var catalogRepository = new CatalogRefsRepository(context);
            var collectionsFactory = new CollectionsFactory(_fakeClock, _guidSource);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var collectionsService = new CollectionsService(
                collectionsFactory,
                collectionsRepository,
                shopsRepository,
                catalogRepository);

            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                factory.Invoke(collectionsService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }
    }
}
