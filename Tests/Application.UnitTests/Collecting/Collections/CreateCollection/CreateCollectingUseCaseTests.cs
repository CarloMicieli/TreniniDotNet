using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.TestInputs.Collecting;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public class CreateCollectingUseCaseTests : CollectingUseCaseTests<CreateCollectionUseCase, CreateCollectionOutput, CreateCollectionOutputPort>
    {
        [Fact]
        public async Task CreateCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewCreateCollection);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewCreateCollection);

            await useCase.Execute(CollectingInputs.CreateCollection.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateCollection_ShouldFail_WhenUserHasAlreadyCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewCreateCollection);

            await useCase.Execute(CollectingInputs.CreateCollection.With(Owner: "George"));

            outputPort.ShouldHaveUserHasAlreadyOneCollectionMessage("The user already owns a collection");
        }

        [Fact]
        public async Task CreateCollection_ShouldCreateCollections()
        {
            var expectedId = Guid.NewGuid();
            SetNextGeneratedGuid(expectedId);

            var (useCase, outputPort, unitOfWork) = ArrangeCollectionUseCase(Start.WithSeedData, NewCreateCollection);

            await useCase.Execute(CollectingInputs.CreateCollection.With(
                Owner: "John",
                Notes: "My notes"));

            outputPort.ShouldHaveStandardOutput();
            outputPort.ShouldHaveNoValidationError();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(expectedId);
            output.Owner.Should().Be("John");
        }

        private CreateCollectionUseCase NewCreateCollection(
            CollectionsService collectionService,
            CreateCollectionOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new CreateCollectionUseCase(outputPort, collectionService, unitOfWork);
        }
    }
}
