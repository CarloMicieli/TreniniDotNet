using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public class AddItemToCollectingUseCaseTests : CollectingUseCaseTests<AddItemToCollectionUseCase, AddItemToCollectionOutput, AddItemToCollectionOutputPort>
    {
        [Fact]
        public async Task AddItemToCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewAddItemToCollection);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewAddItemToCollection);

            await useCase.Execute(CollectingInputs.AddItemToCollection.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewAddItemToCollection);

            var owner = new Owner("not-found");

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = CollectingInputs.AddItemToCollection.With(
                owner: owner,
                catalogItem: "acme-60392",
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForOwner(owner);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewAddItemToCollection);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = CollectingInputs.AddItemToCollection.With(
                owner: "George",
                catalogItem: "acme-60392",
                shop: "Not found",
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage("Not found");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheCatalogItemNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewAddItemToCollection);

            var catalogItem = Slug.Of("acme-123456");
            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = CollectingInputs.AddItemToCollection.With(
                owner: "George",
                catalogItem: catalogItem.ToString(),
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemNotFoundForSlug(catalogItem);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldAddItemsToCollection()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCollectionUseCase(Start.WithSeedData, NewAddItemToCollection);

            var itemId = Guid.NewGuid();
            SetNextGeneratedGuid(itemId);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = CollectingInputs.AddItemToCollection.With(
                owner: "George",
                catalogItem: "acme-60392",
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(new CollectionItemId(itemId));
            output.CatalogItem.Should().Be(Slug.Of("acme-60392"));
        }

        private AddItemToCollectionUseCase NewAddItemToCollection(
            CollectionsService collectionService,
            AddItemToCollectionOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new AddItemToCollectionUseCase(outputPort, collectionService, unitOfWork);
        }
    }
}
