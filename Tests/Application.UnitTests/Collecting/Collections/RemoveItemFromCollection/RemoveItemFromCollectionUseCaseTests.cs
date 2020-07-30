using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public class RemoveItemFromCollectionUseCaseTests : CollectionUseCaseTests<RemoveItemFromCollectionUseCase, RemoveItemFromCollectionInput, RemoveItemFromCollectionOutput, RemoveItemFromCollectionOutputPort>
    {
        [Fact]
        public async Task RemoveItemFromCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewRemoveItemFromCollectionInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldOutputError_WhenCollectionIsNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var collectionId = new CollectionId(Guid.NewGuid());
            var itemId = new CollectionItemId(Guid.NewGuid());
            var input = NewRemoveItemFromCollectionInput.With(
                owner: new Owner("George"),
                id: collectionId,
                itemId: itemId);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundForId(collectionId);
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldRemoveTheItemFromTheCollection()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            var collectionId = collection.Id;
            var itemId = collection.Items.First().Id;

            var input = NewRemoveItemFromCollectionInput.With(
                owner: new Owner("George"),
                id: collectionId,
                itemId: itemId);

            await useCase.Execute(input);

            unitOfWork.EnsureUnitOfWorkWasSaved();

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.CollectionId.Should().Be(collectionId);
            output.ItemId.Should().Be(itemId);
        }

        private RemoveItemFromCollectionUseCase CreateUseCase(
            IRemoveItemFromCollectionOutputPort outputPort,
            CollectionsService collectionsService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork) =>
            new RemoveItemFromCollectionUseCase(new RemoveItemFromCollectionInputValidator(), outputPort, collectionsService, unitOfWork);
    }
}
