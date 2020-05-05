using System;
using TreniniDotNet.Application.InMemory.Collecting.Collections;
using TreniniDotNet.Application.InMemory.Collecting.Common;
using TreniniDotNet.Application.InMemory.Collecting.Shops;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.UseCases.Interfaces.Output;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.Services;

namespace TreniniDotNet.Application.Collecting
{
    public abstract class CollectingUseCaseTests<TUseCase, TUseCaseOutput, TOutputPort>
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
                unitOfWork,
                context);
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
                unitOfWork,
                context);
        }

        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeShopUseCase(
            Start initData,
            Func<ShopsService, TOutputPort, IUnitOfWork, TUseCase> factory)
        {
            var context = NewMemoryContext(initData);

            var shopsRepository = new ShopsRepository(context);
            var shopsFactory = new ShopsFactory(_fakeClock, _guidSource);

            IUnitOfWork unitOfWork = new UnitOfWork();

            var shopsService = new ShopsService(shopsRepository, shopsFactory);

            var outputPort = new TOutputPort();

            return new UseCaseFixture<TUseCase, TOutputPort>(
                factory.Invoke(shopsService, outputPort, unitOfWork),
                outputPort,
                unitOfWork,
                context);
        }
    }
}
