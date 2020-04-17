using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using System;
using System.Linq;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public class EditCollectionItemUseCaseTests : CollectionUseCaseTests<EditCollectionItem, EditCollectionItemOutput, EditCollectionItemOutputPort>
    {
        [Fact]
        public async Task EditCollectionItem_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewEditCollectionItem);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewEditCollectionItem);

            await useCase.Execute(CollectionInputs.EditCollectionItem.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var input = CollectionInputs.EditCollectionItem.With(
                Id: id.ToGuid(),
                ItemId: Guid.NewGuid(),
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionNotFoundMessage($"Collection with id {id} does not exist");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheCollectionItemNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var itemId = Guid.NewGuid();
            var input = CollectionInputs.EditCollectionItem.With(
                Id: id.ToGuid(),
                ItemId: itemId,
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveCollectionItemNotFoundMessage($"Collection item with id {itemId} does not exist");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldFail_WhenTheShopNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var itemId = collection.Items.Select(it => it.ItemId.ToGuid()).First();
            var input = CollectionInputs.EditCollectionItem.With(
                Id: id.ToGuid(),
                ItemId: itemId,
                Shop: "Not found",
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveShopNotFoundMessage($"Shop 'Not found' does not exist");
        }

        [Fact]
        public async Task EditCollectionItem_ShouldEditCatalogItem()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCollectionsUseCase(Start.WithSeedData, NewEditCollectionItem);

            var collection = CollectionSeedData.Collections.GeorgeCollection();
            var id = collection.CollectionId;
            var itemId = collection.Items.Select(it => it.ItemId).First();
            var input = CollectionInputs.EditCollectionItem.With(
                Id: id.ToGuid(),
                ItemId: itemId.ToGuid(),
                Price: 450M);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.CollectionId.Should().Be(id);
            output.ItemId.Should().Be(itemId);
            output.CatalogItem.Should().Be(Slug.Of("acme-60458"));
        }

        private EditCollectionItem NewEditCollectionItem(
            CollectionsService collectionService,
            EditCollectionItemOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new EditCollectionItem(outputPort, collectionService, unitOfWork);
        }
    }
}
