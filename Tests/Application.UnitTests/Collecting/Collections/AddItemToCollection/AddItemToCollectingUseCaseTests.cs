using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.Collecting.Shared;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public class AddItemToCollectingUseCaseTests : CollectionUseCaseTests<AddItemToCollectionUseCase, AddItemToCollectionInput, AddItemToCollectionOutput, AddItemToCollectionOutputPort>
    {
        [Fact]
        public async Task AddItemToCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewAddItemToCollectionInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var owner = new Owner("not-found");

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = NewAddItemToCollectionInput.With(
                owner: owner,
                catalogItem: "acme-60392",
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForOwner(owner);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = NewAddItemToCollectionInput.With(
                owner: "George",
                catalogItem: "acme-60392",
                shop: "Not found",
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage("Not found");
        }

        [Fact]
        public async Task AddItemToCollection_ShouldFail_WhenTheCatalogItemNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var catalogItem = Slug.Of("acme-123456");
            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = NewAddItemToCollectionInput.With(
                owner: "George",
                catalogItem: catalogItem.ToString(),
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemNotFoundForSlug(catalogItem);
        }

        [Fact]
        public async Task AddItemToCollection_ShouldAddItemsToCollection()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var itemId = Guid.NewGuid();
            SetNextGeneratedGuid(itemId);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = NewAddItemToCollectionInput.With(
                owner: "George",
                catalogItem: "acme-60392",
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(new CollectionItemId(itemId));
            output.CatalogItem.Should().Be(Slug.Of("acme-60392"));
        }

        private AddItemToCollectionUseCase CreateUseCase(
            IAddItemToCollectionOutputPort outputPort,
            CollectionsService collectionsService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork) =>
            new AddItemToCollectionUseCase(new AddItemToCollectionInputValidator(), outputPort, collectionsService, collectionItemsFactory, unitOfWork);
    }
}
