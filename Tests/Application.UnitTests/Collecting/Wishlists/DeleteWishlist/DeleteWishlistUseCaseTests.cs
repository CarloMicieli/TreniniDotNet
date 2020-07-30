using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.DeleteWishlist
{
    public class DeleteWishlistUseCaseTests : WishlistUseCaseTests<DeleteWishlistUseCase, DeleteWishlistInput, DeleteWishlistOutput, DeleteWishlistOutputPort>
    {
        [Fact]
        public async Task DeleteWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task DeleteWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewDeleteWishlistInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task DeleteWishlist_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewDeleteWishlistInput.With(
                owner: "George",
                id: Guid.NewGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistNotFound(new WishlistId(input.Id));
        }

        [Fact]
        public async Task DeleteWishlist_ShouldDeleteWishlist()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();
            var input = NewDeleteWishlistInput.With(
                owner: "George",
                id: wishlist.Id);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(wishlist.Id);
        }

        private DeleteWishlistUseCase CreateUseCase(
            IDeleteWishlistOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new DeleteWishlistUseCase(new DeleteWishlistInputValidator(), outputPort, wishlistsService, unitOfWork);
    }
}
