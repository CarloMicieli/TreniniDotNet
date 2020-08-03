using System;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Brands
{
    public sealed class BrandsRepositoryTests : RepositoryUnitTests<IBrandsRepository>
    {
        public BrandsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, unitOfWork => new BrandsRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task BrandsRepository_AddAsync_ShouldInsertNewBrands()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            var testBrand = FakeBrand();
            var brandId = await Repository.AddAsync(testBrand);
            await UnitOfWork.SaveAsync();

            brandId.Should().Be(testBrand.Id);

            Database.Assert.RowInTable(Tables.Brands)
                .WithPrimaryKey(new
                {
                    brand_id = testBrand.Id.ToGuid()
                })
                .WithValues(new
                {
                    slug = testBrand.Slug.ToString(),
                    name = testBrand.Name,
                    version = testBrand.Version,
                })
                .ShouldExists();
        }

        [Fact]
        public async Task BrandsRepository_UpdateAsync_ShouldUpdateBrands()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            var testBrand = FakeBrand();

            Database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = testBrand.Id.ToGuid(),
                name = testBrand.Name,
                slug = testBrand.Slug.Value,
                company_name = testBrand.CompanyName,
                kind = BrandKind.Industrial.ToString(),
                version = 1,
                created = DateTime.UtcNow
            });

            await Repository.UpdateAsync(testBrand.With(companyName: "Modified company"));
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.Brands)
                .WithPrimaryKey(new
                {
                    brand_id = testBrand.Id.ToGuid()
                })
                .AndValues(new
                {
                    version = 2,
                    company_name = "Modified company"
                });
        }

        [Fact]
        public async Task BrandsRepository_GetBySlugAsync_ShouldFindOneBrandBySlug()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            Database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = Guid.NewGuid(),
                name = "A.C.M.E.",
                slug = "acme",
                company_name = "Associazione Costruzioni Modellistiche Esatte",
                kind = BrandKind.Industrial.ToString(),
                version = 1,
                created = DateTime.UtcNow
            });

            var brand = await Repository.GetBySlugAsync(Slug.Of("acme"));

            brand.Should().NotBeNull();
            brand?.Slug.Should().Be(Slug.Of("acme"));
        }

        [Fact]
        public async Task BrandsRepository_GetBrandsAsync_ShouldFindBrandsPaginated()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            Database.Arrange.InsertMany("brands", 20, id =>
                new
                {
                    brand_id = Guid.NewGuid(),
                    name = $"Brand{id}",
                    slug = $"brand-{id}",
                    company_name = $"company name {id}",
                    kind = BrandKind.Industrial.ToString(),
                    version = 1,
                    created = DateTime.UtcNow
                });

            var result = await Repository.GetBrandsAsync(new Page(10, 5));

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(5);
        }

        [Fact]
        public async Task BrandsRepository_ExistsAsync_ShouldReturnTrueWhenBrandExists()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            Database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = Guid.NewGuid(),
                name = "A.C.M.E.",
                slug = "acme",
                company_name = "Associazione Costruzioni Modellistiche Esatte",
                kind = BrandKind.Industrial.ToString(),
                version = 1,
                created = DateTime.UtcNow
            });

            var exists = await Repository.ExistsAsync(Slug.Of("acme"));

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task BrandsRepository_ExistsAsync_ShouldReturnFalseWhenBrandDoesNotExist()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            var exists = await Repository.ExistsAsync(Slug.Of("acme"));

            exists.Should().BeFalse();
        }

        private static Brand FakeBrand() => CatalogSeedData.Brands.New()
            .Id(new Guid("2dfc8a61-8218-44af-8be5-d012bde4cf03"))
            .Name("ACME")
            .WebsiteUrl(new Uri("http://localhost"))
            .MailAddress(new MailAddress("mail@mail.com"))
            .CompanyName("Associazione Costruzioni Modellistiche Esatte")
            .Industrial()
            .Build();
    }
}