using Xunit;
using FluentAssertions;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.Infrastructure.Dapper;

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

            var testBrand = new TestBrand();
            var brandId = await Repository.AddAsync(testBrand);

            brandId.Should().Be(testBrand.BrandId);

            Database.Assert.RowInTable(Tables.Brands)
                .WithPrimaryKey(new
                {
                    brand_id = testBrand.BrandId.ToGuid()
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
    }

    public sealed class TestBrand : IBrand
    {
        public BrandId BrandId => new BrandId(new Guid("2dfc8a61-8218-44af-8be5-d012bde4cf03"));

        public Slug Slug => Slug.Of("acme");

        public string Name => "A.C.M.E.";

        public Uri WebsiteUrl => new Uri("http://localhost");

        public MailAddress EmailAddress => new MailAddress("mail@mail.com");

        public string CompanyName => "Associazione Costruzioni Modellistiche Esatte";

        public string GroupName => null;

        public string Description => null;

        public Address Address => null;

        public BrandKind Kind => BrandKind.Industrial;

        public Instant CreatedDate =>
            Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1988, 11, 25), DateTimeKind.Utc));

        public Instant? ModifiedDate => null;

        public int Version => 42;

        public IBrandInfo ToBrandInfo() => this;
    }
}
