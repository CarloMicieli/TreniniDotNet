using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Domain.Collecting.Shops
{
    public class ShopsServiceTests
    {
        private Guid ExpectedId { get; }

        private ShopsService Service { get; }
        private Mock<IShopsRepository> RepositoryMock { get; }

        public ShopsServiceTests()
        {
            ExpectedId = Guid.NewGuid();

            RepositoryMock = new Mock<IShopsRepository>();

            var factory = Factories<ShopsFactory>
                .New((clock, guidSource) => new ShopsFactory(clock, guidSource))
                .Id(ExpectedId)
                .Build();

            Service = new ShopsService(factory, RepositoryMock.Object);
        }

        [Fact]
        public async Task ShopsService_ExistsAsync_ShouldCheckWhetherShopExists()
        {
            var shop = CollectingSeedData.Shops.ModellbahnshopLippe();

            RepositoryMock.Setup(x => x.ExistsAsync(shop.Slug))
                .ReturnsAsync(true);

            var exist1 = await Service.ExistsAsync(shop.Slug);
            var exist2 = await Service.ExistsAsync(Slug.Of("not found"));

            exist1.Should().BeTrue();
            exist2.Should().BeFalse();
        }

        [Fact]
        public async Task ShopsService_CreateShopAsync_ShouldCreateNewShops()
        {
            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Shop>()))
                .ReturnsAsync(new ShopId(ExpectedId));

            var shopId = await Service.CreateShopAsync(
                "Shop name",
                new Uri("http://www.shop.com"),
                new MailAddress("mail@shop.com"),
                null,
                null);

            shopId.Should().Be(new ShopId(ExpectedId));
        }

        [Fact]
        public async Task ShopsService_GetBySlugAsync_ShouldReturnShopsByTheirSlug()
        {
            var shop = CollectingSeedData.Shops.ModellbahnshopLippe();

            RepositoryMock.Setup(x => x.GetBySlugAsync(shop.Slug))
                .ReturnsAsync(shop);

            var shop1 = await Service.GetBySlugAsync(shop.Slug);
            var shop2 = await Service.GetBySlugAsync(Slug.Of("not found"));

            shop1.Should().NotBeNull();
            shop1?.Id.Should().Be(shop.Id);
            shop1?.Slug.Should().Be(shop.Slug);

            shop2.Should().BeNull();
        }

        [Fact]
        public async Task ShopsService_GetAllShopsAsync_ShouldReturnShops()
        {
            var page = new Page();

            RepositoryMock.Setup(x => x.GetAllAsync(page))
                .ReturnsAsync(new PaginatedResult<Shop>(page, new List<Shop>()));

            var results = await Service.GetAllShopsAsync(page);

            results.Should().NotBeNull();
            results.Results.Should().HaveCount(0);
        }
    }
}