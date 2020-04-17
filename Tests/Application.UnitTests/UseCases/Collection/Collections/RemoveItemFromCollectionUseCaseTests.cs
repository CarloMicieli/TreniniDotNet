using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using TreniniDotNet.Domain.Collection.ValueObjects;
using System;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using System.Linq;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public class RemoveItemFromCollectionUseCaseTests :
        CollectionUseCaseTests<RemoveItemFromCollection, RemoveItemFromCollectionOutput, RemoveItemFromCollectionOutputPort>
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

            await useCase.Execute(CollectionInputs.RemoveItemFromCollection.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldOutputError_WhenCollectionIsNotFound()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewRemoveItemFromCollection);

            var collectionId = new CollectionId(Guid.NewGuid());
            var itemId = new CollectionItemId(Guid.NewGuid());
            var input = CollectionInputs.RemoveItemFromCollection.With(
                Id: collectionId.ToGuid(),
                ItemId: itemId.ToGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionItemWasNotFoundForId(collectionId, itemId);
        }

        [Fact]
        public async Task RemoveItemFromCollection_ShouldRemoveTheItemFromTheCollection()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCollectionUseCase(Start.WithSeedData, NewRemoveItemFromCollection);

            var collection = CollectionSeedData.Collections.GeorgeCollection();

            var collectionId = collection.CollectionId;
            var itemId = collection.Items.First().ItemId;

            var input = CollectionInputs.RemoveItemFromCollection.With(
                Id: collectionId.ToGuid(),
                ItemId: itemId.ToGuid());

            await useCase.Execute(input);

            unitOfWork.EnsureUnitOfWorkWasSaved();

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Should().NotBeNull();
            output.CollectionId.Should().Be(collectionId);
            output.ItemId.Should().Be(itemId);
        }

        private RemoveItemFromCollection NewRemoveItemFromCollection(
            CollectionsService collectionService,
            RemoveItemFromCollectionOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new RemoveItemFromCollection(outputPort, collectionService, unitOfWork);
        }
    }
}
