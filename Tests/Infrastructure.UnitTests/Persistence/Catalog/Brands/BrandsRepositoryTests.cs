using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using System;
using TreniniDotNet.Common;
using TreniniDotNet.Infrastructure.Database.Testing;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Infrastracture.Dapper;
using NodaTime;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public class BrandsRepositoryTests : RepositoryUnitTests<IBrandsRepository>
    {
        public BrandsRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IBrandsRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new BrandsRepository(databaseContext, new BrandsFactory(clock));

        [Fact]
        public async Task BrandsRepository_Add_ShouldInsertNewBrands()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            var testBrand = new TestBrand();
            var brandId = await Repository.Add(testBrand);

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
                version = 1,
                created_at = DateTime.UtcNow
            });

            var brand = await Repository.GetBySlug(Slug.Of("acme"));

            brand.Should().NotBeNull();
            brand.Slug.Should().Be(Slug.Of("acme"));
        }

        [Fact]
        public async Task BrandsRepository_GetByName_ShouldFindOneBrandByName()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            Database.Arrange.InsertOne(Tables.Brands, new
            {
                brand_id = Guid.NewGuid(),
                name = "A.C.M.E.",
                slug = "acme",
                company_name = "Associazione Costruzioni Modellistiche Esatte",
                version = 1,
                created_at = DateTime.UtcNow
            });

            var brand = await Repository.GetByName("A.C.M.E.");

            brand.Should().NotBeNull();
            brand.Name.Should().Be("A.C.M.E.");
        }

        [Fact]
        public async Task BrandsRepository_GetAll_ShouldFindAllBrands()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            Database.Arrange.Insert("brands",
                new
                {
                    brand_id = Guid.NewGuid(),
                    name = "A.C.M.E.",
                    slug = "acme",
                    company_name = "Associazione Costruzioni Modellistiche Esatte",
                    version = 1,
                    created_at = DateTime.UtcNow
                },
                new
                {
                    brand_id = Guid.NewGuid(),
                    name = "Roco",
                    slug = "roco",
                    company_name = "Roco AG",
                    version = 1,
                    created_at = DateTime.UtcNow
                }
                );

            var brands = await Repository.GetAll();

            brands.Should().NotBeNull();
            brands.Should().HaveCount(2);
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
                    version = 1,
                    created_at = DateTime.UtcNow
                });

            var result = await Repository.GetBrands(new Page(10, 5));

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
                version = 1,
                created_at = DateTime.UtcNow
            });

            bool exists = await Repository.Exists(Slug.Of("acme"));

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task BrandsRepository_ExistsBySlug_ShouldReturnFalseWhenBrandDoesNotExist()
        {
            Database.Setup.TruncateTable(Tables.Brands);

            bool exists = await Repository.Exists(Slug.Of("acme"));

            exists.Should().BeFalse();
        }
    }

    public sealed class TestBrand : IBrand
    {
        public Uri WebsiteUrl => new Uri("http://localhost");

        public MailAddress EmailAddress => new MailAddress("mail@mail.com");

        public string CompanyName => "Associazione Costruzioni Modellistiche Esatte";

        public BrandKind Kind => BrandKind.Industrial;

        public DateTime? CreatedAt => new DateTime(1988, 11, 25);

        public int? Version => 42;

        public BrandId BrandId => new BrandId(new Guid("2dfc8a61-8218-44af-8be5-d012bde4cf03"));

        public Slug Slug => Slug.Of("acme");

        public string Name => "A.C.M.E.";

        public IBrandInfo ToBrandInfo()
        {
            throw new NotImplementedException();
        }
    }
}
