using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Infrastructure.Database.Testing;
using NodaTime;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Railways
{
    public class RailwaysRepositoryTests : RepositoryUnitTests<IRailwaysRepository>
    {
        public RailwaysRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IRailwaysRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new RailwaysRepository(databaseContext, new RailwaysFactory(clock, new GuidSource()));

        [Fact]
        public async Task RailwaysRepository_Add_ShouldCreateANewRailway()
        {
            Database.Setup.TruncateTable(Tables.Railways);

            var fs = new TestRailway();

            var railwayId = await Repository.Add(fs);

            railwayId.Should().NotBeNull();
            Database.Assert.RowInTable(Tables.Railways)
                .WithPrimaryKey(new
                {
                    railway_id = fs.RailwayId.ToGuid()
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
        public async Task RailwaysRepository_Exists_ShouldReturnTrueWhenRailwayExists()
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
        public async Task RailwaysRepository_GetBySlug_ShouldReturnTheRailwayWhenExists()
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
            railway.Slug.Should().Be(expectedSlug);
        }

        [Fact]
        public async Task RailwaysRepository_GetBySlug_ShouldReturnNullWhenTheRailwayDoesNotExist()
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
        public async Task RailwaysRepository_GetBrands_ShouldReturnTheRailwaysPaginated()
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
    }

    public class TestRailway : IRailway
    {
        public RailwayId RailwayId => new RailwayId(new Guid("b9e62d18-06e3-4404-9183-6c3a3b89c683"));

        public Slug Slug => Slug.Of("FS");

        public string Name => "FS";

        public string CompanyName => "Ferrovie dello Stato";

        public int Version => 42;

        public Country Country => Country.Of("IT");

        public PeriodOfActivity PeriodOfActivity => PeriodOfActivity.ActiveRailway(new DateTime(1905, 7, 1));

        public RailwayGauge TrackGauge => null;

        public RailwayLength TotalLength => null;

        public Uri WebsiteUrl => new Uri("https://www.trenitalia.com");

        public string Headquarters => null;

        public Instant CreatedDate => Instant.FromUtc(1988, 11, 25, 9, 0);

        public Instant? ModifiedDate => null;

        public IRailwayInfo ToRailwayInfo()
        {
            throw new NotImplementedException();
        }
    }
}
