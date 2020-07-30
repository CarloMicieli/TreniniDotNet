using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using Infrastructure.UnitTests.Persistence.Database.Testing;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace Infrastructure.UnitTests.Persistence.Catalog
{
    public class BrandsRepositoryTests : EfRepositoryUnitTests<BrandsRepository>
    {
        public BrandsRepositoryTests()
            : base(context => new BrandsRepository(context))
        {
        }

        [Fact]
        public async Task BrandsRepository_AddAsync_ShouldInsertNewBrands()
        {
            var testBrand = TestBrand();

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context);
                var brandId = await repo.AddAsync(testBrand);

                var affectedRows = await context.SaveChangesAsync();

                affectedRows.Should().Be(1);
                brandId.Should().Be(testBrand.Id);
            }

            await using (var context = NewDbContext())
            {
                var brand = context.Brands.FirstOrDefault(it => it.Id == testBrand.Id);
                brand.Should().NotBeNull();
                brand?.Address.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task BrandsRepository_UpdateAsync_ShouldUpdateBrands()
        {
            Brand brand = null;

            await using (var context = NewDbContext())
            {
                var repo = await Repository(context, Create.WithSeedData);

                brand = await context.Brands.FirstOrDefaultAsync();

                var modified = brand.With(name: brand.Name + "(2)");
                await repo.UpdateAsync(modified);

                await context.SaveChangesAsync();
            }

            await using (var context = NewDbContext())
            {
                var brandFound = await context.Brands
                    .FirstOrDefaultAsync(it => it.Id == brand.Id);

                brandFound.Name.Should().Be(brand.Name + "(2)");
            }
        }

        [Fact]
        public async Task BrandsRepository_GetBySlugAsync_ShouldFindOneBrandBySlug()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var brand1 = await repo.GetBySlugAsync(Slug.Of("ACME"));
            var brand2 = await repo.GetBySlugAsync(Slug.Of("not found"));

            brand1.Should().NotBeNull();
            brand1?.Slug.Should().Be(Slug.Of("ACME"));

            brand2.Should().BeNull();
        }

        [Fact]
        public async Task BrandsRepository_GetAllAsync_ShouldFindBrandsPaginated()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var page = new Page(1, 5);
            var results = await repo.GetAllAsync(page);

            results.Should().NotBeNull();
            results.CurrentPage.Should().Be(page);
            results.Results.Should().HaveCount(5);
        }

        [Fact]
        public async Task BrandsRepository_ExistsAsync_ShouldFindOneBrandBySlug()
        {
            await using var context = NewDbContext();
            var repo = await Repository(context, Create.WithSeedData);

            var found1 = await repo.ExistsAsync(Slug.Of("ACME"));
            var found2 = await repo.ExistsAsync(Slug.Of("not found"));

            found1.Should().BeTrue();
            found2.Should().BeFalse();
        }

        [Fact]
        public async Task BrandsRepository_DeleteAsync_ShouldDeleteBrandsByTheirId()
        {
            var brandId = BrandId.Empty;
            await using (var context = NewDbContext())
            {
                var repo = await Repository(context, Create.WithSeedData);

                brandId = await context.Brands
                    .Select(it => it.Id)
                    .FirstOrDefaultAsync();

                await repo.DeleteAsync(brandId);

                await context.SaveChangesAsync();
            }

            await using (var context = NewDbContext())
            {
                var exists = await context.Brands
                    .AnyAsync(it => it.Id == brandId);

                exists.Should().BeFalse();
            }
        }

        [Fact]
        public async Task BrandsRepository_DeleteAsync_ShouldDeleteBrands()
        {
            Brand brand = null;
            await using (var context = NewDbContext())
            {
                var repo = await Repository(context, Create.WithSeedData);

                brand = await context.Brands
                    .FirstOrDefaultAsync();

                await repo.DeleteAsync(brand);

                await context.SaveChangesAsync();
            }

            await using (var context = NewDbContext())
            {
                var exists = await context.Brands
                    .AnyAsync(it => it.Id == brand.Id);

                exists.Should().BeFalse();
            }
        }

        private static Brand TestBrand() => CatalogSeedData.Brands.New()
            .Id(new Guid("2dfc8a61-8218-44af-8be5-d012bde4cf03"))
            .Name("Test brand")
            .WebsiteUrl(new Uri("http://localhost"))
            .MailAddress(new MailAddress("mail@mail.com"))
            .CompanyName("Test brand company")
            .Address(Address.With(
                "Address lin1",
                "Address line2",
                city: "City",
                postalCode: "123456",
                country: "DE"))
            .Kind(BrandKind.Industrial)
            .Build();
    }
}
