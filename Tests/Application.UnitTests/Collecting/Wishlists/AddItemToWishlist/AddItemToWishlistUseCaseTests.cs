using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.Collecting.Wishlists.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public class AddItemToWishlistUseCaseTests : CollectingUseCaseTests<AddItemToWishlistUseCase, AddItemToWishlistOutput, AddItemToWishlistOutputPort>
    {
        [Fact]
        public async Task AddItemToWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewAddItemToWishlist);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewAddItemToWishlist);

            await useCase.Execute(CollectingInputs.AddItemToWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewAddItemToWishlist);

            var id = Guid.NewGuid();
            var input = CollectingInputs.AddItemToWishlist.With(
                owner: "George",
                id: id,
                catalogItem: "acme-123456");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistNotFound(new WishlistId(id));
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenCatalogItemWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewAddItemToWishlist);

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;
            var input = CollectingInputs.AddItemToWishlist.With(
                owner: "George",
                id: id.ToGuid(),
                catalogItem: "acme-123456");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemNotFound(Slug.Of("acme-123456"));
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenCatalogItemWasAlreadyPresentInTheList()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewAddItemToWishlist);

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var id = wishlist.WishlistId;
            var item = wishlist.Items.First();

            var input = CollectingInputs.AddItemToWishlist.With(
                owner: "George",
                id: id.ToGuid(),
                catalogItem: item.CatalogItem.Slug.ToString());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemAlreadyPresent(id, item.ItemId, item.CatalogItem);
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldAddNewItems()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeWishlistUseCase(Start.WithSeedData, NewAddItemToWishlist);

            var wishlist = CollectionSeedData.Wishlists.George_First_List();
            var id = wishlist.WishlistId;
            var item = CatalogSeedData.CatalogItems.Bemo_1254134();

            var itemId = Guid.NewGuid();
            SetNextGeneratedGuid(itemId);

            var input = CollectingInputs.AddItemToWishlist.With(
                owner: "George",
                id: id.ToGuid(),
                catalogItem: item.Slug.ToString());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(id);
            output.ItemId.Should().Be(new WishlistItemId(itemId));
        }

        private AddItemToWishlistUseCase NewAddItemToWishlist(
            WishlistService wishlistsService,
            AddItemToWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new AddItemToWishlistUseCase(outputPort, wishlistsService, unitOfWork);
        }
    }
}
