using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemsServiceTests
    {
        private Guid ExpectedId { get; }

        private CatalogItemsService Service { get; }
        private Mock<ICatalogItemsRepository> RepositoryMock { get; }
        private Mock<IBrandsRepository> BrandsRepositoryMock { get; }
        private Mock<IRailwaysRepository> RailwaysRepositoryMock { get; }
        private Mock<IScalesRepository> ScalesRepositoryMock { get; }

        public CatalogItemsServiceTests()
        {
            ExpectedId = Guid.NewGuid();

            RepositoryMock = new Mock<ICatalogItemsRepository>();
            BrandsRepositoryMock = new Mock<IBrandsRepository>();
            RailwaysRepositoryMock = new Mock<IRailwaysRepository>();
            ScalesRepositoryMock = new Mock<IScalesRepository>();

            var factory = Factories<CatalogItemsFactory>
                .New((clock, guidSource) => new CatalogItemsFactory(clock, guidSource))
                .Id(ExpectedId)
                .Build();

            Service = new CatalogItemsService(factory,
                RepositoryMock.Object,
                BrandsRepositoryMock.Object,
                RailwaysRepositoryMock.Object,
                ScalesRepositoryMock.Object);
        }

        [Fact]
        public async Task CatalogItemsService_GetBySlugAsync_ShouldFindCatalogItemBySlug()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            RepositoryMock.Setup(x => x.GetBySlugAsync(item.Slug))
                .ReturnsAsync(item);

            var itemResult1 = await Service.GetBySlugAsync(item.Slug);
            var itemResult2 = await Service.GetBySlugAsync(Slug.Of("not found"));

            itemResult1.Should().NotBeNull();
            itemResult1?.Slug.Should().Be(item.Slug);

            itemResult2.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemsService_FindRailwayBySlug_ShouldFindRailwayBySlug()
        {
            var fs = CatalogSeedData.Railways.Fs();

            RailwaysRepositoryMock.Setup(x => x.GetBySlugAsync(fs.Slug))
                .ReturnsAsync(fs);

            var railway1 = await Service.FindRailwayBySlug(fs.Slug);
            var railway2 = await Service.FindRailwayBySlug(Slug.Of("not found"));

            railway1.Should().NotBeNull();
            railway1?.Slug.Should().Be(fs.Slug);

            railway2.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemsService_FindBrandBySlug_ShouldFindBrandBySlug()
        {
            var acme = CatalogSeedData.Brands.Acme();

            BrandsRepositoryMock.Setup(x => x.GetBySlugAsync(acme.Slug))
                .ReturnsAsync(acme);

            var brand1 = await Service.FindBrandBySlug(acme.Slug);
            var brand2 = await Service.FindBrandBySlug(Slug.Of("not found"));

            brand1.Should().NotBeNull();
            brand1?.Slug.Should().Be(acme.Slug);

            brand2.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemsService_ItemAlreadyExists_ShouldCheckIfCatalogItemAlreadyExists()
        {
            var acme = CatalogSeedData.Brands.Acme();
            var itemNumber = new ItemNumber("123456");

            RepositoryMock.Setup(x => x.ExistsAsync(acme, itemNumber))
                .ReturnsAsync(true);

            var result1 = await Service.ItemAlreadyExists(acme, itemNumber);
            var result2 = await Service.ItemAlreadyExists(acme, new ItemNumber("654321"));

            result1.Should().BeTrue();
            result2.Should().BeFalse();
        }

        [Fact]
        public async Task CatalogItemsService_FindScaleBySlug_ShouldFindScaleBySlug()
        {
            var scaleH0 = CatalogSeedData.Scales.ScaleH0();

            ScalesRepositoryMock.Setup(x => x.GetBySlugAsync(scaleH0.Slug))
                .ReturnsAsync(scaleH0);

            var scale1 = await Service.FindScaleBySlug(scaleH0.Slug);
            var scale2 = await Service.FindScaleBySlug(Slug.Of("not found"));

            scale1.Should().NotBeNull();
            scale1?.Slug.Should().Be(scaleH0.Slug);

            scale2.Should().BeNull();
        }

        [Fact]
        public async Task CatalogItemsService_FindRailwaysBySlug_ShouldFindMultipleRailwaysBySlug()
        {
            var fs = CatalogSeedData.Railways.Fs();
            var db = CatalogSeedData.Railways.DieBahn();

            RailwaysRepositoryMock.Setup(x => x.GetBySlugAsync(fs.Slug))
                .ReturnsAsync(fs);
            RailwaysRepositoryMock.Setup(x => x.GetBySlugAsync(db.Slug))
                .ReturnsAsync(db);

            var railways = new List<Slug> { fs.Slug, db.Slug };

            var (found, notFound) = await Service.FindRailwaysBySlug(railways);

            found.Should().HaveCount(2);
            notFound.Should().HaveCount(0);
        }

        [Fact]
        public async Task CatalogItemsService_CreateCatalogItem_ShouldCreateNewCatalogItem()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<CatalogItem>()))
                .ReturnsAsync(new CatalogItemId(ExpectedId));

            var (id, slug) = await Service.CreateCatalogItem(
                item.Brand,
                item.ItemNumber,
                item.Scale,
                item.PowerMethod,
                item.Description,
                item.PrototypeDescription,
                item.ModelDescription,
                item.DeliveryDate,
                item.IsAvailable,
                item.RollingStocks);

            id.Should().Be(new CatalogItemId(ExpectedId));
            slug.Should().Be(item.Slug);
        }

        [Fact]
        public async Task CatalogItemsService_UpdateCatalogItem_ShouldModifyCatalogItems()
        {
            var item = CatalogSeedData.CatalogItems.Acme_60392();

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<CatalogItem>()))
                .Returns(Task.CompletedTask);

            await Service.UpdateCatalogItemAsync(item);
        }

        [Fact]
        public async Task CatalogItemsService_GetLatestCatalogItemsAsync_ShouldFindLatestCatalogItems()
        {
            var page = new Page();

            RepositoryMock.Setup(x => x.GetLatestCatalogItemsAsync(page))
                .ReturnsAsync(new PaginatedResult<CatalogItem>(page, new List<CatalogItem>()));

            var results = await Service.GetLatestCatalogItemsAsync(page);

            results.Should().NotBeNull();
        }
    }
}
