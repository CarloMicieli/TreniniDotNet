using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.TestInputs.Collecting;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.EditWishlistItem
{
    public class EditWishlistItemUseCaseTests : CollectingUseCaseTests<EditWishlistItemUseCase, EditWishlistItemOutput, EditWishlistItemOutputPort>
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

            await useCase.Execute(CollectingInputs.EditWishlistItem.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task EditWishlistItem_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewEditWishlistItem);

            var input = CollectingInputs.EditWishlistItem.With(
                Owner: "George",
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

            var input = CollectingInputs.EditWishlistItem.With(
                Owner: "George",
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

        private EditWishlistItemUseCase NewEditWishlistItem(
            WishlistService wishlistsService,
            EditWishlistItemOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new EditWishlistItemUseCase(outputPort, wishlistsService, unitOfWork);
        }
    }
}
