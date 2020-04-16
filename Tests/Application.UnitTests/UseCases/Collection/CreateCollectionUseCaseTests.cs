using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using System;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public class CreateCollectionUseCaseTests : UseCaseTestHelper<CreateCollection, CreateCollectionOutput, CreateCollectionOutputPort>
    {
        [Fact]
        public async Task CreateCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewCreateCollection);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewCreateCollection);

            await useCase.Execute(CollectionInputs.CreateCollection.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateCollection_ShouldFail_WhenUserHasAlreadyCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewCreateCollection);

            await useCase.Execute(CollectionInputs.CreateCollection.With(Owner: "George"));

            outputPort.ShouldHaveUserHasAlreadyOneCollectionMessage("The user already owns a collection");
        }

        [Fact]
        public async Task CreateCollection_ShouldCreateCollections()
        {
            var expectedId = Guid.NewGuid();
            SetNextId(expectedId);

            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewCreateCollection);

            await useCase.Execute(CollectionInputs.CreateCollection.With(
                Owner: "John",
                Notes: "My notes"));

            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(expectedId);
            output.Owner.Should().Be("John");
        }

        private CreateCollection NewCreateCollection(
            CollectionsService collectionService,
            CreateCollectionOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new CreateCollection(outputPort, collectionService, unitOfWork);
        }
    }
}
