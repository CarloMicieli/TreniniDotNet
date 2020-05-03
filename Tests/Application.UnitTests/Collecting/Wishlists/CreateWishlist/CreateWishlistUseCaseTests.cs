using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public class CreateWishlistUseCaseTests : CollectingUseCaseTests<CreateWishlistUseCase, CreateWishlistOutput, CreateWishlistOutputPort>
    {
        [Fact]
        public async Task CreateWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewCreateWishlist);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewCreateWishlist);

            await useCase.Execute(CollectingInputs.CreateWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateWishlist_ShouldOutputError_WhenAlreadyExistsWishlistWithTheSameSlug()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewCreateWishlist);

            var input = CollectingInputs.CreateWishlist.With(
                owner: "George",
                listName: "First list",
                visibility: "Private");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistAlreadyExistsWithSlug(Slug.Of("First list"));
        }

        [Fact]
        public async Task CreateWishlist_ShouldCreateNewWishlist()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewCreateWishlist);

            var guid = Guid.NewGuid();
            SetNextGeneratedGuid(guid);

            var input = CollectingInputs.CreateWishlist.With(
                owner: "George",
                listName: "Second list",
                visibility: "Private");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.WishlistId.Should().Be(new WishlistId(guid));
            output.Slug.Should().Be(Slug.Of("Second list"));
        }

        private CreateWishlistUseCase NewCreateWishlist(
            WishlistService wishlistsService,
            CreateWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new CreateWishlistUseCase(outputPort, wishlistsService, unitOfWork);
        }
    }
}
