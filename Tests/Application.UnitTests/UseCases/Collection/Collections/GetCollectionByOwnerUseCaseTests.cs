using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public class GetCollectionByOwnerUseCaseTests : CollectionUseCaseTests<GetCollectionByOwner, GetCollectionByOwnerOutput, GetCollectionByOwnerOutputPort>
    {
        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewGetCollectionByOwner);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewGetCollectionByOwner);

            await useCase.Execute(new GetCollectionByOwnerInput("  "));

            outputPort.ShouldHaveValidationErrors();
            outputPort.ShouldHaveValidationErrorFor("Owner");
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputError_WhenCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.Empty, NewGetCollectionByOwner);

            await useCase.Execute(new GetCollectionByOwnerInput("Not found"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundFor(new Owner("Not found"));
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputTheCollection()
        {
            var (useCase, outputPort) = ArrangeCollectionsUseCase(Start.WithSeedData, NewGetCollectionByOwner);

            await useCase.Execute(new GetCollectionByOwnerInput("George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Collection.Should().NotBeNull();
            output.Collection.Owner.Should().Be(new Owner("George"));
        }

        private GetCollectionByOwner NewGetCollectionByOwner(
            CollectionsService collectionService,
            GetCollectionByOwnerOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new GetCollectionByOwner(outputPort, collectionService);
        }
    }
}
