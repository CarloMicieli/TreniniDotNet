using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Collections.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public class GetCollectingByOwnerUseCaseTests : CollectingUseCaseTests<GetCollectionByOwnerUseCase, GetCollectionByOwnerOutput, GetCollectionByOwnerOutputPort>
    {
        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewGetCollectionByOwner);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewGetCollectionByOwner);

            await useCase.Execute(new GetCollectionByOwnerInput(Guid.Empty, "  "));

            outputPort.ShouldHaveValidationErrors();
            outputPort.ShouldHaveValidationErrorFor("Owner");
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputError_WhenCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.Empty, NewGetCollectionByOwner);

            await useCase.Execute(new GetCollectionByOwnerInput(Guid.Empty, "Not found"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundFor(new Owner("Not found"));
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputTheCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionUseCase(Start.WithSeedData, NewGetCollectionByOwner);

            var collection = CollectionSeedData.Collections.GeorgeCollection();

            await useCase.Execute(new GetCollectionByOwnerInput(collection.Id.ToGuid(), "George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Collection.Should().NotBeNull();
            output.Collection.Owner.Should().Be(new Owner("George"));
        }

        private GetCollectionByOwnerUseCase NewGetCollectionByOwner(
            CollectionsService collectionService,
            GetCollectionByOwnerOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetCollectionByOwnerUseCase(outputPort, collectionService);
        }
    }
}
