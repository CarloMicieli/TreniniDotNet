using System;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentAssertions;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Infrastructure.Dapper;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public class BrandsRepositoryTests : RepositoryUnitTests<IBrandsRepository>
    {
        public BrandsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IBrandsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new BrandsRepository(databaseContext, new BrandsFactory(clock, new GuidSource()));

        [Fact]
        public async Task BrandsRepository_Add_ShouldInsertNewBrands()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            var testBrand = FakeBrand();
            var brandId = await Repository.AddAsync(testBrand);

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
        public async Task BrandsRepository_Update_ShouldUpdateBrands()
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
        public async Task BrandsRepository_GetBySlug_ShouldFindOneBrandBySlug()
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
            brand.Slug.Should().Be(Slug.Of("acme"));
        }

        [Fact]
        public async Task BrandsRepository_GetInfoBySlug_ShouldFindBrandInfoBySlug()
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

            var notFound = await Repository.GetInfoBySlugAsync(Slug.Of("Not Found"));
            var brandInfo = await Repository.GetInfoBySlugAsync(Slug.Of("acme"));

            brandInfo.Should().NotBeNull();
            brandInfo.Slug.Should().Be(Slug.Of("acme"));

            notFound.Should().BeNull();
        }

        [Fact]
        public async Task BrandsRepository_GetBrands_ShouldFindBrandsPaginated()
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
        public async Task BrandsRepository_ExistsBySlug_ShouldReturnTrueWhenBrandExists()
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

            bool exists = await Repository.ExistsAsync(Slug.Of("acme"));

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task BrandsRepository_ExistsBySlug_ShouldReturnFalseWhenBrandDoesNotExist()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            bool exists = await Repository.ExistsAsync(Slug.Of("acme"));

            exists.Should().BeFalse();
        }

        private static IBrand FakeBrand() => CatalogSeedData.NewBrandWith(
            new BrandId(new Guid("2dfc8a61-8218-44af-8be5-d012bde4cf03")),
            "ACME",
            websiteUrl: new Uri("http://localhost"),
            mailAddress: new MailAddress("mail@mail.com"),
            companyName: "Associazione Costruzioni Modellistiche Esatte",
            brandKind: BrandKind.Industrial);
    }
}
