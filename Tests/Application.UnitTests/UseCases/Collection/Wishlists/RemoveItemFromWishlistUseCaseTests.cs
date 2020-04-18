using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using System;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using System.Linq;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class RemoveItemFromWishlistUseCaseTests : CollectionUseCaseTests<RemoveItemFromWishlist, RemoveItemFromWishlistOutput, RemoveItemFromWishlistOutputPort>
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

            await useCase.Execute(CollectionInputs.RemoveItemFromWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldOutputError_WhenItemWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewRemoveItemFromWishlist);

            var input = CollectionInputs.RemoveItemFromWishlist.With(
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid());

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

            var input = CollectionInputs.RemoveItemFromWishlist.With(
                Id: wishlist.WishlistId.ToGuid(),
                ItemId: item.ItemId.ToGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(wishlist.WishlistId);
            output.ItemId.Should().Be(item.ItemId);
        }

        private RemoveItemFromWishlist NewRemoveItemFromWishlist(
            WishlistService collectionService,
            RemoveItemFromWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new RemoveItemFromWishlist(outputPort, collectionService, unitOfWork);
        }
    }
}
