using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public class RemoveItemFromWishlistUseCaseTests : WishlistUseCaseTests<RemoveItemFromWishlistUseCase, RemoveItemFromWishlistInput, RemoveItemFromWishlistOutput, RemoveItemFromWishlistOutputPort>
    {
        [Fact]
        public async Task RemoveItemFromWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewRemoveItemFromWishlistInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldOutputError_WhenItemWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var input = NewRemoveItemFromWishlistInput.With(
                owner: "George",
                id: Guid.NewGuid(),
                itemId: Guid.NewGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistItemNotFound(
                new WishlistId(input.Id),
                new WishlistItemId(input.ItemId));
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldDeleteItems()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var item = wishlist.Items.First();

            var input = NewRemoveItemFromWishlistInput.With(
                owner: "George",
                id: wishlist.Id,
                itemId: item.Id);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(wishlist.Id);
            output.ItemId.Should().Be(item.Id);
        }

        private RemoveItemFromWishlistUseCase CreateUseCase(
            IRemoveItemFromWishlistOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new RemoveItemFromWishlistUseCase(new RemoveItemFromWishlistInputValidator(), outputPort, wishlistsService, unitOfWork);
    }
}
