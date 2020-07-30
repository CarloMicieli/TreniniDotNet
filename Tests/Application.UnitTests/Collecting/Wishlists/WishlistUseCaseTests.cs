using System;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists
{
    public abstract class WishlistUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort> : AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeUseCase(
            Start initData,
            Func<TOutputPort, WishlistsService, WishlistItemsFactory, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);
            var wishlistItemsFactory = new WishlistItemsFactory(GuidSource);

            var wishlistsService = new WishlistsService(
                new WishlistsFactory(Clock, GuidSource),
                new WishlistsRepository(context),
                new CatalogItemsRepository(context));

            return BuildUseCaseFixture(
                useCaseBuilder(OutputPort, wishlistsService, wishlistItemsFactory, UnitOfWork),
                context);
        }
    }
}

