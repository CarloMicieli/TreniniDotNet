using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public class CreateCollectingUseCaseTests : CollectionUseCaseTests<CreateCollectionUseCase, CreateCollectionInput, CreateCollectionOutput, CreateCollectionOutputPort>
    {
        [Fact]
        public async Task CreateCollection_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateCollection_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewCreateCollectionInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateCollection_ShouldFail_WhenUserHasAlreadyCollection()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(NewCreateCollectionInput.With(owner: "George"));

            outputPort.ShouldHaveUserHasAlreadyOneCollectionMessage(new Owner("George"));
        }

        [Fact]
        public async Task CreateCollection_ShouldCreateCollections()
        {
            var expectedId = Guid.NewGuid();
            SetNextGeneratedGuid(expectedId);

            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            await useCase.Execute(NewCreateCollectionInput.With(
                owner: "John",
                notes: "My notes"));

            outputPort.ShouldHaveStandardOutput();
            outputPort.ShouldHaveNoValidationError();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(expectedId);
            output.Owner.Should().Be("John");
        }

        private CreateCollectionUseCase CreateUseCase(
            ICreateCollectionOutputPort outputPort,
            CollectionsService collectionsService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork) =>
            new CreateCollectionUseCase(new CreateCollectionInputValidator(), outputPort, collectionsService, unitOfWork);
    }
}
