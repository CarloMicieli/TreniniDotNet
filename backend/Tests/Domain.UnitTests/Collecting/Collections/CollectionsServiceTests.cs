using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public class CollectionsServiceTests
    {
        private Guid ExpectedId { get; }

        private CollectionsService Service { get; }
        private Mock<ICollectionsRepository> RepositoryMock { get; }
        private Mock<ICatalogItemRefsRepository> CatalogItemsRepositoryMock { get; }
        private Mock<IShopsRepository> ShopsRepositoryMock { get; }

        public CollectionsServiceTests()
        {
            ExpectedId = Guid.NewGuid();

            RepositoryMock = new Mock<ICollectionsRepository>();
            ShopsRepositoryMock = new Mock<IShopsRepository>();
            CatalogItemsRepositoryMock = new Mock<ICatalogItemRefsRepository>();

            var factory = Factories<CollectionsFactory>
                .New((clock, guidSource) => new CollectionsFactory(clock, guidSource))
                .Id(ExpectedId)
                .Build();

            Service = new CollectionsService(
                factory,
                RepositoryMock.Object,
                CatalogItemsRepositoryMock.Object,
                ShopsRepositoryMock.Object);
        }

        [Fact]
        public async Task CollectionsService_GetByOwnerAsync_ShouldReturnCollectionsByTheirOwner()
        {
            var georgeCollection = CollectingSeedData.Collections.NewGeorgeCollection();

            RepositoryMock.Setup(x => x.GetByOwnerAsync(georgeCollection.Owner))
                .ReturnsAsync(georgeCollection);

            var collection1 = await Service.GetByOwnerAsync(georgeCollection.Owner);
            var collection2 = await Service.GetByOwnerAsync(new Owner("not exists"));

            collection1.Should().NotBeNull();
            collection1?.Owner.Should().Be(georgeCollection.Owner);
            collection1?.Id.Should().Be(georgeCollection.Id);

            collection2.Should().BeNull();
        }

        [Fact]
        public async Task CollectionsService_GetShopBySlugAsync_ShouldReturnShopsByTheirSlug()
        {
            var shop = CollectingSeedData.Shops.NewTecnomodelTreni();

            ShopsRepositoryMock.Setup(x => x.GetBySlugAsync(shop.Slug))
                .ReturnsAsync(shop);

            var shop1 = await Service.GetShopBySlugAsync(shop.Slug);
            var shop2 = await Service.GetShopBySlugAsync(Slug.Of("not found"));

            shop1.Should().NotBeNull();
            shop1?.Id.Should().Be(shop.Id);
            shop1?.Slug.Should().Be(shop.Slug);

            shop2.Should().BeNull();
        }

        [Fact]
        public async Task CollectionsService_GetCatalogItemAsync_ShouldReturnCatalogItemsByTheirSlug()
        {
            var item = CatalogSeedData.CatalogItems.NewAcme60392();

            CatalogItemsRepositoryMock.Setup(x => x.GetCatalogItemAsync(item.Slug))
                .ReturnsAsync(new CatalogItemRef(item));

            var catalogItem1 = await Service.GetCatalogItemAsync(item.Slug);
            var catalogItem2 = await Service.GetCatalogItemAsync(Slug.Of("not empty"));

            catalogItem1.Should().NotBeNull();
            catalogItem1?.Id.Should().Be(item.Id);
            catalogItem1?.Slug.Should().Be(item.Slug);

            catalogItem2.Should().BeNull();
        }

        [Fact]
        public async Task CollectionsService_UpdateCollectionAsync_ShouldUpdateCollections()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            await Service.UpdateCollectionAsync(collection);
        }

        [Fact]
        public async Task CollectionsService_ExistsByOwnerAsync_ShouldCheckWhetherCollectionExistForTheOwner()
        {
            var collection = CollectingSeedData.Collections.NewGeorgeCollection();

            RepositoryMock.Setup(x => x.ExistsAsync(collection.Owner))
                .ReturnsAsync(true);

            var found1 = await Service.ExistsByOwnerAsync(collection.Owner);
            var found2 = await Service.ExistsByOwnerAsync(new Owner("not found"));

            found1.Should().BeTrue();
            found2.Should().BeFalse();
        }

        [Fact]
        public async Task CollectionsService_CreateAsync_ShouldCreateNewCollections()
        {
            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Collection>()))
                .ReturnsAsync(new CollectionId(ExpectedId));

            var id = await Service.CreateAsync(new Owner("George"), "My collection notes");
            id.Should().Be(new CollectionId(ExpectedId));
        }
    }
}