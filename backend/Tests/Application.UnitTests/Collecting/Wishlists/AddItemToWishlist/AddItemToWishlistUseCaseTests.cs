using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public class AddItemToWishlistUseCaseTests : WishlistUseCaseTests<AddItemToWishlistUseCase, AddItemToWishlistInput, AddItemToWishlistOutput, AddItemToWishlistOutputPort>
    {
        [Fact]
        public async Task AddItemToWishlist_ShouldOutputAnError_WhenInputIsNull()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(null);

            outputPort.ShouldHaveErrorMessage("The use case input is null");
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            await useCase.Execute(NewAddItemToWishlistInput.Empty);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenWishlistWasNotFound()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.Empty, CreateUseCase);

            var id = Guid.NewGuid();
            var input = NewAddItemToWishlistInput.With(
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
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var id = CollectingSeedData.Wishlists.NewGeorgeFirstList().Id;
            var input = NewAddItemToWishlistInput.With(
                owner: "George",
                id: id,
                catalogItem: "acme-123456");

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemNotFound(Slug.Of("acme-123456"));
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldOutputError_WhenCatalogItemWasAlreadyPresentInTheList()
        {
            var (useCase, outputPort) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var id = wishlist.Id;
            var item = wishlist.Items.First();

            var input = NewAddItemToWishlistInput.With(
                owner: "George",
                id: id,
                catalogItem: item.CatalogItem.Slug.ToString());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemAlreadyPresent(id, item.CatalogItem);
        }

        [Fact]
        public async Task AddItemToWishlist_ShouldAddNewItems()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeUseCase(Start.WithSeedData, CreateUseCase);

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();
            var id = wishlist.Id;
            var item = CatalogSeedData.CatalogItems.NewBemo1254134();

            var itemId = Guid.NewGuid();
            SetNextGeneratedGuid(itemId);

            var input = NewAddItemToWishlistInput.With(
                owner: "George",
                id: id,
                catalogItem: item.Slug.ToString());

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            var output = outputPort.UseCaseOutput;
            output.Id.Should().Be(id);
            output.ItemId.Should().Be(new WishlistItemId(itemId));
        }

        private AddItemToWishlistUseCase CreateUseCase(
            IAddItemToWishlistOutputPort outputPort,
            WishlistsService wishlistsService,
            WishlistItemsFactory wishlistItemsFactory,
            IUnitOfWork unitOfWork) =>
            new AddItemToWishlistUseCase(new AddItemToWishlistInputValidator(), outputPort, wishlistsService, wishlistItemsFactory, unitOfWork);
    }
}
