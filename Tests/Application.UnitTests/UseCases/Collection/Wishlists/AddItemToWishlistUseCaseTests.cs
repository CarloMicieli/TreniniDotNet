using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.InMemory.OutputPorts.Collection;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToWishlist;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Application.Services;
using System.Threading.Tasks;
using TreniniDotNet.Application.TestInputs.Collection;
using System;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Common;
using System.Linq;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Application.UseCases.Collection.Wishlists
{
    public class AddItemToWishlistUseCaseTests : CollectionUseCaseTests<AddItemToWishlist, AddItemToWishlistOutput, AddItemToWishlistOutputPort>
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

            await useCase.Execute(CollectionInputs.AddItemToWishlist.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.Empty, NewAddItemToWishlist);

            var id = Guid.NewGuid();
            var input = CollectionInputs.AddItemToWishlist.With(
                Owner: "George",
                Id: id,
                CatalogItem: "acme-123456");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertWishlistNotFound(new WishlistId(id));
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenCatalogItemWasNotFound()
        {
            var (useCase, outputPort) = ArrangeWishlistUseCase(Start.WithSeedData, NewAddItemToWishlist);

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;
            var input = CollectionInputs.AddItemToWishlist.With(
                Owner: "George",
                Id: id.ToGuid(),
                CatalogItem: "acme-123456");

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

            var input = CollectionInputs.AddItemToWishlist.With(
                Owner: "George",
                Id: id.ToGuid(),
                CatalogItem: item.CatalogItem.Slug.ToString());

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

            var input = CollectionInputs.AddItemToWishlist.With(
                Owner: "George",
                Id: id.ToGuid(),
                CatalogItem: item.Slug.ToString());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(id);
            output.ItemId.Should().Be(new WishlistItemId(itemId));
        }

        private AddItemToWishlist NewAddItemToWishlist(
            WishlistService wishlistsService,
            AddItemToWishlistOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new AddItemToWishlist(outputPort, wishlistsService, unitOfWork);
        }
    }
}
