using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public class GetCollectionByOwnerUseCaseTests : CollectionUseCaseTests<GetCollectionByOwnerUseCase, GetCollectionByOwnerInput, GetCollectionByOwnerOutput, GetCollectionByOwnerOutputPort>
    {
        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetCollectionByOwnerInput(Guid.Empty, "  "));

            outputPort.ShouldHaveValidationErrors();
            outputPort.ShouldHaveValidationErrorFor("Owner");
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputError_WhenCollectionNotExists()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(new GetCollectionByOwnerInput(Guid.Empty, "Not found"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCollectionWasNotFoundFor(new Owner("Not found"));
        }

        [Fact]
        public async Task GetCollectionByOwner_ShouldOutputTheCollection()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await useCase.Execute(new GetCollectionByOwnerInput(collection.Id, "George"));

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            var output = outputPort.UseCaseOutput;
            output.Collection.Should().NotBeNull();
            output.Collection.Owner.Should().Be(new Owner("George"));
        }

        private GetCollectionByOwnerUseCase CreateUseCase(
            IGetCollectionByOwnerOutputPort outputPort,
            CollectionsService collectionsService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork) =>
            new GetCollectionByOwnerUseCase(new GetCollectionByOwnerInputValidator(), outputPort, collectionsService);
    }
}
