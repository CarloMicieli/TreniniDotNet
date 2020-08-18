using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.CreateWishlist
{
    public class CreateWishlistUseCaseTests : WishlistUseCaseTests<CreateWishlistUseCase, CreateWishlistInput, CreateWishlistOutput, CreateWishlistOutputPort>
    {
        [Fact]
        public async Task CreateWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task CreateWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewCreateWishlistInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateWishlist_ShouldOutputError_WhenAlreadyExistsWishlistWithTheSameSlug()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var input = NewCreateWishlistInput.With(
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
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var guid = Guid.NewGuid();
            SetNextGeneratedGuid(guid);

            var input = NewCreateWishlistInput.With(
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

        private CreateWishlistUseCase CreateUseCase(
            ICreateWishlistOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new CreateWishlistUseCase(new CreateWishlistInputValidator(), outputPort, wishlistsService, unitOfWork);
    }
}
