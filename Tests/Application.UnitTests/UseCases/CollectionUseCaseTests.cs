using System;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Application.InMemory.Repositories.Collection;
using TreniniDotNet.Application.InMemory.Services;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Wishlists;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class CollectionUseCaseTests<TUseCase, TUseCaseOutput, TOutputPort>
        : UseCaseTestHelper<TUseCase, TUseCaseOutput, TOutputPort>
            where TUseCaseOutput : IUseCaseOutput
            where TOutputPort : IOutputPortStandard<TUseCaseOutput>, new()
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeCollectionUseCase(
            Start initData,
            Func<CollectionsService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = NewMemoryContext(initData);

            var collectionsRepository = new CollectionsRepository(context);
            var collectionItemsRepository = new CollectionItemsRepository(context);
            var shopsRepository = new ShopsRepository(context);
            var catalogRepository = new CatalogRefsRepository(context);
            var collectionsFactory = new CollectionsFactory(_fakeClock, _guidSource);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var collectionsService = new CollectionsService(
                collectionsFactory,
                collectionsRepository,
                collectionItemsRepository,
                shopsRepository,
                catalogRepository);

            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                factory.Invoke(collectionsService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }

        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeWishlistUseCase(
            Start initData,
            Func<WishlistService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = NewMemoryContext(initData);

            var catalogRefsRepository = new CatalogRefsRepository(context);
            var wishlistsRepository = new WishlistsRepository(context);
            var wishlistItemsRepository = new WishlistItemsRepository(context);
            var wishlistsFactory = new WishlistsFactory(_fakeClock, _guidSource);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var wishlistsService = new WishlistService(
                wishlistsRepository,
                wishlistItemsRepository,
                wishlistsFactory,
                catalogRefsRepository);

            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                factory.Invoke(wishlistsService, outputPort, unitOfWork),
                outputPort,
                unitOfWork);
        }
    }
}
