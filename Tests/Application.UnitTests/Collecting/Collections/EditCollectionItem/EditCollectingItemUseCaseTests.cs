using System;
using System.Linq;
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

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public class EditCollectingItemUseCaseTests : CollectionUseCaseTests<EditCollectionItemUseCase, EditCollectionItemInput, EditCollectionItemOutput, EditCollectionItemOutputPort>
    {
        [Fact]
        public async Task EditCollectionItem_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewEditCollectionItemInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = NewEditCollectionItemInput.With(
                owner: collection.Owner.Value,
                id: id,
                itemId: Guid.NewGuid(),
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionNotFoundMessage(collection.Owner, id);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheCollectionItemNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = Guid.NewGuid();
            var input = NewEditCollectionItemInput.With(
                owner: collection.Owner.Value,
                id: id,
                itemId: itemId,
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionItemNotFoundMessage(collection.Owner, id, new CollectionItemId(itemId));
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheUserIsNotTheCollectionOwner()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = Guid.NewGuid();
            var input = NewEditCollectionItemInput.With(
                owner: collection.Owner.Value,
                id: id,
                itemId: itemId,
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionItemNotFoundMessage(collection.Owner, id, new CollectionItemId(itemId));
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = collection.Items.Select(it => it.Id).First();
            var input = NewEditCollectionItemInput.With(
                owner: collection.Owner,
                id: id,
                itemId: itemId,
                shop: "Not found",
                price: NewPriceInput.With(450M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage("Not found");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldEditCatalogItem()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = collection.Items.Select(it => it.Id).First();
            var input = NewEditCollectionItemInput.With(
                owner: collection.Owner,
                id: id,
                itemId: itemId,
                price: NewPriceInput.With(999M, "EUR"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(itemId);
            output.CatalogItem.Should().Be(Slug.Of("acme-60458"));

            var dbCollection = dbContext.Collections.FirstOrDefault(it => it.Id == id);
            var dbCollectionItem = dbCollection?.Items.FirstOrDefault(it => it.Id == itemId);

            dbCollectionItem?.Price.Amount.Should().Be(999M);
        }

        private EditCollectionItemUseCase CreateUseCase(
            IEditCollectionItemOutputPort outputPort,
            CollectionsService collectionsService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork) =>
            new EditCollectionItemUseCase(new EditCollectionItemInputValidator(), outputPort, collectionsService, unitOfWork);
    }
}
