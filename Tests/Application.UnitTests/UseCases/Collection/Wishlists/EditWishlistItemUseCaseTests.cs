using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.EditWishlistItem;
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
    public class EditWishlistItemUseCaseTests : CollectionUseCaseTests<EditWishlistItem, EditWishlistItemOutput, EditWishlistItemOutputPort>
    {
        [Fact]
        public async Task EditWishlistItem_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewEditWishlistItem);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task EditWishlistItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewEditWishlistItem);

            await useCase.Execute(CollectionInputs.EditWishlistItem.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditWishlistItem_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewEditWishlistItem);

            var input = CollectionInputs.EditWishlistItem.With(
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistItemNotFound(
                new WishlistId(input.Id),
                new WishlistItemId(input.ItemId));
        }

        [Fact]
        public async Task EditWishlistItem_ShouldEditWishlistItems()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewEditWishlistItem);

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var item = wishlist.Items.First();

            var input = CollectionInputs.EditWishlistItem.With(
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

        private EditWishlistItem NewEditWishlistItem(
            WishlistService wishlistsService,
            EditWishlistItemOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new EditWishlistItem(outputPort, wishlistsService, unitOfWork);
        }
    }
}
