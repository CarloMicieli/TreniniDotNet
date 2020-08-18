using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.InMemory.Domain;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public class BrandsServiceTests
    {
        private Guid ExpectedId { get; }

        private BrandsService Service { get; }
        private Mock<IBrandsRepository> RepositoryMock { get; }

        public BrandsServiceTests()
        {
            ExpectedId = Guid.NewGuid();
            RepositoryMock = new Mock<IBrandsRepository>();

            var factory = Factories<BrandsFactory>
                .New((clock, guidSource) => new BrandsFactory(clock, guidSource))
                .Id(ExpectedId)
                .Build();

            Service = new BrandsService(RepositoryMock.Object, factory);
        }

        [Fact]
        public async Task BrandsService_CreateBrand_ShouldCreateNewBrand()
        {
            Brand brand = null;
            RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Brand>()))
                .ReturnsAsync(new BrandId(ExpectedId))
                .Callback<Brand>(b => { brand = b; });

            var acme = CatalogSeedData.Brands.NewAcme();

            var brandId = await Service.CreateBrand(
                acme.Name,
                acme.CompanyName,
                acme.GroupName,
                acme.Description,
                acme.WebsiteUrl,
                acme.EmailAddress,
                acme.Kind,
                acme.Address);

            brandId.Should().Be(new BrandId(ExpectedId));

            brand.Name.Should().Be(acme.Name);
            brand.CompanyName.Should().Be(acme.CompanyName);
            brand.GroupName.Should().Be(acme.GroupName);
            brand.Description.Should().Be(acme.Description);
            brand.WebsiteUrl.Should().Be(acme.WebsiteUrl);
            brand.EmailAddress.Should().Be(acme.EmailAddress);
            brand.Kind.Should().Be(acme.Kind);
            brand.Address.Should().Be(acme.Address);
        }

        [Fact]
        public async Task BrandsService_BrandAlreadyExists_ShouldCheckIfTheBrandExists()
        {
            var slug = Slug.Of("acme");
            RepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Slug>()))
                .ReturnsAsync(false);
            RepositoryMock.Setup(x => x.ExistsAsync(slug))
                .ReturnsAsync(true);

            bool slugExists = await Service.BrandAlreadyExists(slug);
            bool notFoundExists = await Service.BrandAlreadyExists(Slug.Of("not found"));

            slugExists.Should().BeTrue();
            notFoundExists.Should().BeFalse();
        }

        [Fact]
        public async Task BrandsService_GetBrandBySlug_ShouldReturnABrandByItsSlug()
        {
            var acme = CatalogSeedData.Brands.NewAcme();

            RepositoryMock.Setup(x => x.GetBySlugAsync(acme.Slug))
                .ReturnsAsync(acme);

            var found1 = await Service.GetBrandBySlug(acme.Slug);
            var found2 = await Service.GetBrandBySlug(Slug.Of("not found"));

            found1.Should().NotBeNull();
            found1.Slug.Should().Be(acme.Slug);

            found2.Should().BeNull();
        }

        [Fact]
        public async Task BrandsService_UpdateBrand_ShouldUpdateBrandValues()
        {
            var acme = CatalogSeedData.Brands.NewAcme();
            Brand argument = null;

            RepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Brand>()))
                .Returns(Task.CompletedTask)
                .Callback<Brand>(b => { argument = b; });

            await Service.UpdateBrand(acme);

            argument.Should().NotBeNull();
            argument.Slug.Should().Be(acme.Slug);
        }

        [Fact]
        public async Task BrandsService_FindAllBrands_ShouldReturnTheBrands()
        {
            var page = new Page();
            RepositoryMock.Setup(x => x.GetBrandsAsync(It.IsAny<Page>()))
                .ReturnsAsync(new PaginatedResult<Brand>(page, new List<Brand>()));

            var results = await Service.FindAllBrands(page);

            results.Should().NotBeNull();
        }
    }
}
