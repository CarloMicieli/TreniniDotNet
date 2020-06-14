using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public class RemoveItemFromCollectingUseCaseTests :
        CollectingUseCaseTests<RemoveItemFromCollectionUseCase, RemoveItemFromCollectionOutput, RemoveItemFromCollectionOutputPort>
    {
        [Fact]
        public async Task RemoveItemFromCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewRemoveItemFromCollection);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewRemoveItemFromCollection);

            await useCase.Execute(CollectingInputs.RemoveItemFromCollection.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldOutputError_WhenCollectionIsNotFound()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewRemoveItemFromCollection);

            var collectionId = new CollectionId(Guid.NewGuid());
            var itemId = new CollectionItemId(Guid.NewGuid());
            var input = CollectingInputs.RemoveItemFromCollection.With(
                owner: new Owner("George"),
                id: collectionId.ToGuid(),
                itemId: itemId.ToGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionItemWasNotFoundForId(collectionId, itemId);
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldRemoveTheItemFromTheCollection()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCollectionUseCase(Start.WithSeedData, NewRemoveItemFromCollection);

            var collection = CollectionSeedData.Collections.GeorgeCollection();

            var collectionId = collection.Id;
            var itemId = collection.Items.First().Id;

            var input = CollectingInputs.RemoveItemFromCollection.With(
                owner: new Owner("George"),
                id: collectionId.ToGuid(),
                itemId: itemId.ToGuid());

            await useCase.Execute(input);

            unitOfWork.EnsureUnitOfWorkWasSaved();

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.CollectionId.Should().Be(collectionId);
            output.ItemId.Should().Be(itemId);
        }

        private RemoveItemFromCollectionUseCase NewRemoveItemFromCollection(
            CollectionsService collectionService,
            RemoveItemFromCollectionOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new RemoveItemFromCollectionUseCase(outputPort, collectionService, unitOfWork);
        }
    }
}
