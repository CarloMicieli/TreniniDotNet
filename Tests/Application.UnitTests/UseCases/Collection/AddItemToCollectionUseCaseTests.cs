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

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class AddItemToCollectionUseCaseTests : UseCaseTestHelper<AddItemToCollection, AddItemToCollectionOutput, AddItemToCollectionOutputPort>
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

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Id: id.ToGuid(),
                Brand: "ACME",
                ItemNumber: "123456",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionNotFoundMessage($"Collection with id {id} does not exist");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewAddItemToCollection);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Id: id.ToGuid(),
                Brand: "ACME",
                ItemNumber: "123456",
                Shop: "Not found",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage($"Shop 'Not found' does not exist");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldAddItemsToCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewAddItemToCollection);

            var itemId = Guid.NewGuid();
            SetNextId(itemId);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.AddItemToCollection.With(
                Id: id.ToGuid(),
                Brand: "ACME",
                ItemNumber: "123456",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(new CollectionItemId(itemId));
            output.CatalogItem.Should().Be(Slug.Of("acme-123456"));
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
