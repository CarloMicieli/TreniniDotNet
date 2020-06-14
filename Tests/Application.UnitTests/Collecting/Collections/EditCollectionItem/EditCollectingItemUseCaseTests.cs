using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public class EditCollectingItemUseCaseTests : CollectingUseCaseTests<EditCollectionItemUseCase, EditCollectionItemOutput, EditCollectionItemOutputPort>
    {
        [Fact]
        public async Task EditCollectionItem_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewEditCollectionItem);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewEditCollectionItem);

            await useCase.Execute(CollectingInputs.EditCollectionItem.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var input = CollectingInputs.EditCollectionItem.With(
                owner: collection.Owner.Value,
                id: id.ToGuid(),
                itemId: Guid.NewGuid(),
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionNotFoundMessage(collection.Owner, id);
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheCollectionItemNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = Guid.NewGuid();
            var input = CollectingInputs.EditCollectionItem.With(
                owner: collection.Owner.Value,
                id: id.ToGuid(),
                itemId: itemId,
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionItemNotFoundMessage(collection.Owner, id, new CollectionItemId(itemId));
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheUserIsNotTheCollectionOwner()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = Guid.NewGuid();
            var input = CollectingInputs.EditCollectionItem.With(
                owner: collection.Owner.Value,
                id: id.ToGuid(),
                itemId: itemId,
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionItemNotFoundMessage(collection.Owner, id, new CollectionItemId(itemId));
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = collection.Items.Select(it => it.Id.ToGuid()).First();
            var input = CollectingInputs.EditCollectionItem.With(
                owner: collection.Owner,
                id: id.ToGuid(),
                itemId: itemId,
                shop: "Not found",
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage("Not found");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldEditCatalogItem()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCollectionUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.Id;
            var itemId = collection.Items.Select(it => it.Id).First();
            var input = CollectingInputs.EditCollectionItem.With(
                owner: collection.Owner,
                id: id.ToGuid(),
                itemId: itemId.ToGuid(),
                price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(itemId);
            output.CatalogItem.Should().Be(Slug.Of("acme-60458"));
        }

        private EditCollectionItemUseCase NewEditCollectionItem(
            CollectionsService collectionService,
            EditCollectionItemOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new EditCollectionItemUseCase(outputPort, collectionService, unitOfWork);
        }
    }
}
