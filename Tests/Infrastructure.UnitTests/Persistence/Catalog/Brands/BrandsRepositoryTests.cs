using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using NodaTime;
using System;
using NodaTime.Testing;
using TreniniDotNet.Common;
using TreniniDotNet.Infrastructure.Database.Testing;
using System.Net.Mail;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public class BrandsRepositoryTests : IClassFixture<SqliteDatabaseFixture>
    {
        private const string Brands = "brands";

        private readonly IBrandsRepository BrandsRepository;
        private DatabaseTestHelpers Database { get; }

        public BrandsRepositoryTests(SqliteDatabaseFixture fixture)
        {
            var fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 9, 0));
            var factory = new BrandsFactory(fakeClock);

            Database = new DatabaseTestHelpers(fixture.DatabaseContext);
            BrandsRepository = new BrandsRepository(fixture.DatabaseContext, factory);
        }

        [Fact]
        public async Task BrandsRepository_Should_InsertNewBrands()
        {
            Database.Setup.TruncateTable(Brands);

            var testBrand = new TestBrand();
            var brandId = await BrandsRepository.Add(testBrand);

            brandId.Should().Be(testBrand.BrandId);

            Database.Assert.RowIn(Brands)
                .WithPrimaryKey(new
                {
                    brand_id = testBrand.BrandId.ToGuid()
                })
                .WithValues(new
                {
                    slug = "acme",
                    name = "A.C.M.E."
                })
                .ShouldExists();
        }

        [Fact]
        public async Task BrandsRepository_ShouldFindOneBrandBySlug()
        {
            Database.Setup.TruncateTable(Brands);

            Database.Arrange.InsertOne(Brands, new
            {
                brand_id = Guid.NewGuid(),
                name = "A.C.M.E.",
                slug = "acme",
                company_name = "Associazione Costruzioni Modellistiche Esatte",
                version = 1,
                created_at = DateTime.UtcNow
            });

            var brand = await BrandsRepository.GetBySlug(Slug.Of("acme"));

            brand.Should().NotBeNull();
            brand.Slug.Should().Be(Slug.Of("acme"));
        }

        [Fact]
        public async Task BrandsRepository_ShouldFindAllBrands()
        {
            Database.Setup.TruncateTable(Brands);

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

            var brands = await BrandsRepository.GetAll();

            brands.Should().NotBeNull();
            brands.Should().HaveCount(2);
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
