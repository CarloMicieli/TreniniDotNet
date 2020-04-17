using Xunit;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class AddItemToCollectionUseCaseTests : CollectionUseCaseTests<AddItemToCollection, AddItemToCollectionOutput, AddItemToCollectionOutputPort>
    {
        [Fact]
        public async Task AddItemToCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewAddItemToCollection);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewAddItemToCollection);

            await useCase.Execute(CollectionInputs.AddItemToCollection.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewAddItemToCollection);

            var owner = new Owner("not-found");

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Owner: owner,
                CatalogItem: "acme-60392",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForOwner(owner);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewAddItemToCollection);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Owner: "George",
                CatalogItem: "acme-60392",
                Shop: "Not found",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage("Not found");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheCatalogItemNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewAddItemToCollection);

            var catalogItem = Slug.Of("acme-123456");
            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Owner: "George",
                CatalogItem: catalogItem.ToString(),
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemNotFoundForSlug(catalogItem);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldAddItemsToCollection()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCollectionsUseCase(Start.WithSeedData, NewAddItemToCollection);

            var itemId = Guid.NewGuid();
            SetNextGeneratedGuid(itemId);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Owner: "George",
                CatalogItem: "acme-60392",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(new CollectionItemId(itemId));
            output.CatalogItem.Should().Be(Slug.Of("acme-60392"));
        }

        private AddItemToCollection NewAddItemToCollection(
            CollectionsService collectionService,
            AddItemToCollectionOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new AddItemToCollection(outputPort, collectionService, unitOfWork);
        }
    }
}
