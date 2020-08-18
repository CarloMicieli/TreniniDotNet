using System;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Application.Collecting.Shops;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public abstract class CollectionUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort> : AbstractUseCaseTests<TUseCase, TUseCaseInput, TUseCaseOutput, TOutputPort>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TOutputPort : IStandardOutputPort<TUseCaseOutput>, new()
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected UseCaseFixture<TUseCase, TOutputPort> ArrangeUseCase(
            Start initData,
            Func<TOutputPort, CollectionsService, CollectionItemsFactory, IUnitOfWork, TUseCase> useCaseBuilder)
        {
            var context = NewMemoryContext(initData);
            var collectionItemsFactory = new CollectionItemsFactory(GuidSource);

            var collectionsService = new CollectionsService(
                new CollectionsFactory(Clock, GuidSource),
                new CollectionsRepository(context),
                new CatalogItemRefsRepository(context),
                new ShopsRepository(context));

            return BuildUseCaseFixture(
                useCaseBuilder(OutputPort, collectionsService, collectionItemsFactory, UnitOfWork),
                context);
        }
    }
}

