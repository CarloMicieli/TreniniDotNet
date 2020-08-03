using System;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Railways
{
    public class RailwaysRepositoryTests : RepositoryUnitTests<IRailwaysRepository>
    {
        public RailwaysRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, unitOfWork => new RailwaysRepository(unitOfWork))
        {
        }

        [Fact]
        public async Task RailwaysRepository_AddAsync_ShouldCreateANewRailway()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            var fs = FakeRailway();

            var railwayId = await Repository.AddAsync(fs);
            await UnitOfWork.SaveAsync();

            railwayId.Should().NotBeNull();
            Database.Assert.RowInTable(Tables.Railways)
                .WithPrimaryKey(new
                {
                    railway_id = fs.Id.ToGuid()
                })
                .AndValues(new
                {
                    name = fs.Name,
                    company_name = fs.CompanyName,
                    slug = fs.Slug.ToString(),
                    country = fs.Country.Code,
                    operating_since = fs.PeriodOfActivity.OperatingSince,
                    operating_until = fs.PeriodOfActivity.OperatingUntil,
                    active = RailwayStatus.Active == fs.PeriodOfActivity.RailwayStatus,
                    // last_modified = fs.LastModifiedAt?.ToDateTimeUtc(),
                    version = fs.Version
                })
                .ShouldExists();
        }

        [Fact]
        public async Task RailwaysRepository_UpdateAsync_ShouldUpdateRailways()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            var testRailway = FakeRailway();

            Database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = testRailway.Id.ToGuid(),
                slug = testRailway.Slug.Value,
                name = testRailway.Name,
                company_name = testRailway.CompanyName,
                created = DateTime.UtcNow,
                version = 1
            });

            await Repository.UpdateAsync(testRailway.With(companyName: "Trenitalia"));
            await UnitOfWork.SaveAsync();

            Database.Assert.RowInTable(Tables.Railways)
                .WithPrimaryKey(new
                {
                    railway_id = testRailway.Id.ToGuid()
                })
                .AndValues(new
                {
                    company_name = testRailway.CompanyName,
                    version = 2
                });
        }

        [Fact]
        public async Task RailwaysRepository_ExistsAsync_ShouldReturnTrueWhenRailwayExists()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            Database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = Guid.NewGuid(),
                slug = Slug.Of("FS").ToString(),
                name = "FS",
                created = DateTime.UtcNow,
                version = 1
            });

            var exists = await Repository.ExistsAsync(Slug.Of("FS"));

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task RailwaysRepository_GetBySlugAsync_ShouldReturnTheRailwayWhenExists()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            Slug expectedSlug = Slug.Of("FS");

            Database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = Guid.NewGuid(),
                slug = expectedSlug.ToString(),
                name = "FS",
                country = "IT",
                active = true,
                created = DateTime.UtcNow,
                version = 1
            });

            var railway = await Repository.GetBySlugAsync(expectedSlug);

            railway.Should().NotBeNull();
            railway?.Slug.Should().Be(expectedSlug);
        }

        [Fact]
        public async Task RailwaysRepository_GetBySlugAsync_ShouldReturnNullWhenTheRailwayDoesNotExist()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            Database.Arrange.InsertOne(Tables.Railways, new
            {
                railway_id = Guid.NewGuid(),
                slug = Slug.Of("FS").ToString(),
                name = "FS",
                country = "IT",
                created = DateTime.UtcNow,
                version = 1
            });

            var railway = await Repository.GetBySlugAsync(Slug.Of("not found"));

            railway.Should().BeNull();
        }

        [Fact]
        public async Task RailwaysRepository_GetRailwaysAsync_ShouldReturnTheRailwaysPaginated()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            Database.Arrange.InsertMany(Tables.Railways, 20, id =>
                new
                {
                    railway_id = Guid.NewGuid(),
                    slug = Slug.Of($"Railway{id}").ToString(),
                    name = $"Railway{id}",
                    country = "IT",
                    created = DateTime.UtcNow,
                    active = true,
                    version = 1
                });

            var result = await Repository.GetRailwaysAsync(new Page(10, 5));

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(5);
        }

        private static Railway FakeRailway()
        {
            return CatalogSeedData.Railways.New()
                .Id(new Guid("b9e62d18-06e3-4404-9183-6c3a3b89c683"))
                .Name("FS")
                .CompanyName("Ferrovie dello Stato")
                .Country(Country.Of("IT"))
                .PeriodOfActivity(PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1)))
                .WebsiteUrl(new Uri("https://www.trenitalia.com"))
                .Build();
        }
    }
}