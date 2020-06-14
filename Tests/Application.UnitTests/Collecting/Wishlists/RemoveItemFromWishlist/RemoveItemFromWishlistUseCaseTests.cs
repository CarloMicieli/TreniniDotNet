using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public class RemoveItemFromWishlistUseCaseTests : CollectingUseCaseTests<RemoveItemFromWishlistUseCase, RemoveItemFromWishlistOutput, RemoveItemFromWishlistOutputPort>
    {
        [Fact]
        public async Task RemoveItemFromWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewRemoveItemFromWishlist);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewRemoveItemFromWishlist);

            await useCase.Execute(CollectingInputs.RemoveItemFromWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldOutputError_WhenItemWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewRemoveItemFromWishlist);

            var input = CollectingInputs.RemoveItemFromWishlist.With(
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
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewRemoveItemFromWishlist);

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var item = wishlist.Items.First();

            var input = CollectingInputs.RemoveItemFromWishlist.With(
                owner: "George",
                id: wishlist.Id.ToGuid(),
                itemId: item.Id.ToGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(wishlist.Id);
            output.ItemId.Should().Be(item.Id);
        }

        private RemoveItemFromWishlistUseCase NewRemoveItemFromWishlist(
            WishlistService wishlistsService,
            RemoveItemFromWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new RemoveItemFromWishlistUseCase(outputPort, wishlistsService, unitOfWork);
        }
    }
}
