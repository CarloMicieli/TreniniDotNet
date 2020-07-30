using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Wishlists
{
    public class WishlistsServiceTests
    {
        private WishlistId ExpectedId { get; }

        private WishlistsService Service { get; }
        private Mock<IWishlistsRepository> RepositoryMock { get; }
        private Mock<ICatalogItemsRepository> CatalogItemsRepositoryMock { get; }

        public WishlistsServiceTests()
        {
            var newId = Guid.NewGuid();
            ExpectedId = new WishlistId(newId);

            RepositoryMock = new Mock<IWishlistsRepository>();
            CatalogItemsRepositoryMock = new Mock<ICatalogItemsRepository>();

            var factory = Factories<WishlistsFactory>
                .New((clock, guidSource) => new WishlistsFactory(clock, guidSource))
                .Id(newId)
                .Build();

            Service = new WishlistsService(factory,
                RepositoryMock.Object,
                CatalogItemsRepositoryMock.Object);
        }

        [Fact]
        public async Task WishlistsService_GetByIdAsync_ShouldReturnWishlistsByTheirId()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            RepositoryMock.Setup(x => x.GetByIdAsync(wishlist.Id))
                .ReturnsAsync(wishlist);

            var wishlist1 = await Service.GetByIdAsync(wishlist.Id);
            var wishlist2 = await Service.GetByIdAsync(new WishlistId());

            wishlist1.Should().NotBeNull();
            wishlist1?.Id.Should().Be(wishlist.Id);

            wishlist2.Should().BeNull();
        }

        [Fact]
        public async Task WishlistsService_GetWishlistsByOwnerAsync_ShouldReturnWishlistsByTheirOwners()
        {
            var criteria = VisibilityCriteria.All;
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            RepositoryMock.Setup(x => x.GetByOwnerAsync(wishlist.Owner, criteria))
                .ReturnsAsync(new List<Wishlist> { wishlist });

            var results = await Service.GetWishlistsByOwnerAsync(wishlist.Owner, criteria);

            results.Should().HaveCount(1);
        }

        [Fact]
        public async Task WishlistsService_CreateWishlistAsync_ShouldCreateNewWishlists()
        {
            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Wishlist>()))
                .ReturnsAsync(ExpectedId);

            var wishlistId = await Service.CreateWishlistAsync(
                new Owner("George"),
                "list name",
                Visibility.Public,
                new Budget(1000M, "EUR"));

            wishlistId.Should().Be(ExpectedId);
        }

        [Fact]
        public async Task WishlistsService_UpdateWishlistAsync_ShouldUpdateWishlists()
        {
            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Wishlist>()))
                .Returns(Task.CompletedTask);

            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();
            await Service.UpdateWishlistAsync(wishlist);
        }

        [Fact]
        public async Task WishlistsService_DeleteWishlistAsync_ShouldDeleteWishlists()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            RepositoryMock.Setup(x => x.DeleteAsync(wishlist.Id))
                .Returns(Task.CompletedTask);

            await Service.DeleteWishlistAsync(wishlist.Id);
        }

        [Fact]
        public async Task WishlistsService_ExistsAsync_ShouldCheckWhetherWishlistExists()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            RepositoryMock.Setup(x => x.ExistsAsync(wishlist.Id))
                .ReturnsAsync(true);

            var exist1 = await Service.ExistsAsync(wishlist.Id);
            var exist2 = await Service.ExistsAsync(new WishlistId());

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsService_ExistsAsync_ShouldCheckWhetherWishlistNameExistsForOwner()
        {
            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            RepositoryMock.Setup(x => x.ExistsAsync(wishlist.Owner, wishlist.ListName))
                .ReturnsAsync(true);

            var exist1 = await Service.ExistsAsync(wishlist.Owner, wishlist.ListName!);
            var exist2 = await Service.ExistsAsync(wishlist.Owner, "not found");

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }

        [Fact]
        public async Task WishlistsService_GetCatalogItemAsync_ShouldFindCatalogItemsByTheirSlug()
        {
            var catalogItem = CatalogSeedData.CatalogItems.Acme_60392();

            CatalogItemsRepositoryMock.Setup(x => x.GetBySlugAsync(catalogItem.Slug))
                .ReturnsAsync(catalogItem);

            var item1 = await Service.GetCatalogItemAsync(catalogItem.Slug);
            var item2 = await Service.GetCatalogItemAsync(Slug.Of("not found"));

            item1.Should().NotBeNull();
            item1?.Slug.Should().Be(catalogItem.Slug);

            item2.Should().BeNull();
        }

        [Fact]
        public async Task WishlistsService_GenerateWishlistName_ShouldGenerateWishlistNames()
        {
            var owner = new Owner("George");

            RepositoryMock.Setup(x => x.CountWishlistsAsync(owner))
                .ReturnsAsync(1);

            var listName1 = await Service.GenerateWishlistName(owner, "My list name");
            var listName2 = await Service.GenerateWishlistName(owner, null);

            listName1.Should().Be("My list name");
            listName2.Should().Be("George's wish list 2");
        }
    }
}
