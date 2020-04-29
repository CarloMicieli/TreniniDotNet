using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Database.Testing;
using NodaTime;
using System;
using TreniniDotNet.Common;
using System.Threading.Tasks;
using TreniniDotNet.Common.Pagination;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public class ScalesRepositoryTests : RepositoryUnitTests<IScalesRepository>
    {
        public ScalesRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IScalesRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new ScalesRepository(databaseContext, new ScalesFactory(clock, new GuidSource()));

        [Fact]
        public async Task ScalesRepository_Add_ShouldCreateANewScale()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new FakeScale();

            var scaleId = await Repository.AddAsync(scaleH0);

            scaleId.Should().NotBeNull();

            Database.Assert.RowInTable(Tables.Scales)
                .WithPrimaryKey(new
                {
                    scale_id = scaleH0.ScaleId.ToGuid()
                })
                .AndValues(new
                {
                    name = scaleH0.Name,
                    slug = scaleH0.Slug.ToString(),
                    ratio = scaleH0.Ratio.ToDecimal(),
                    gauge_mm = scaleH0.Gauge.InMillimeters.Value,
                    gauge_in = scaleH0.Gauge.InInches.Value,
                    track_type = scaleH0.Gauge.TrackGauge.ToString(),
                    description = scaleH0.Description,
                    //last_modified = scaleH0.LastModifiedAt?.ToDateTimeUtc(),
                    version = scaleH0.Version
                })
                .ShouldExists();
        }

        [Fact]
        public async Task ScalesRepository_Update_ShouldUpdateScales()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new FakeScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge_mm = scaleH0.Gauge.InMillimeters.Value,
                gauge_in = scaleH0.Gauge.InInches.Value,
                track_type = scaleH0.Gauge.TrackGauge.ToString(),
                description = scaleH0.Description,
                created = scaleH0.CreatedDate.ToDateTimeUtc(),
                version = scaleH0.Version
            });

            await Repository.UpdateAsync(scaleH0.With(ratio: 100M));

            Database.Assert.RowInTable(Tables.Scales)
                .WithPrimaryKey(new
                {
                    scale_id = scaleH0.ScaleId.ToGuid()
                })
                .AndValues(new
                {
                    version = 2,
                    ratio = 100M
                });
        }

        [Fact]
        public async Task ScalesRepository_GetBySlug_ShouldReturnScaleWithSlug()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new FakeScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge_mm = scaleH0.Gauge.InMillimeters.Value,
                gauge_in = scaleH0.Gauge.InInches.Value,
                track_type = scaleH0.Gauge.TrackGauge.ToString(),
                description = scaleH0.Description,
                created = scaleH0.CreatedDate.ToDateTimeUtc(),
                version = scaleH0.Version
            });

            var scale = await Repository.GetBySlugAsync(scaleH0.Slug);

            scale.Should().NotBeNull();
            scale.Slug.Should().Be(scaleH0.Slug);
        }

        [Fact]
        public async Task ScalesRepository_GetInfoBySlug_ShouldFindScaleInfoBySlug()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new FakeScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge_mm = scaleH0.Gauge.InMillimeters.Value,
                gauge_in = scaleH0.Gauge.InInches.Value,
                track_type = scaleH0.Gauge.TrackGauge.ToString(),
                description = scaleH0.Description,
                created = scaleH0.CreatedDate.ToDateTimeUtc(),
                version = scaleH0.Version
            });

            var notFound = await Repository.GetInfoBySlugAsync(Slug.Of("not found"));
            var scale = await Repository.GetInfoBySlugAsync(scaleH0.Slug);

            scale.Should().NotBeNull();
            scale.Slug.Should().Be(scaleH0.Slug);

            notFound.Should().BeNull();
        }

        [Fact]
        public async Task ScalesRepository_Exists_ShouldReturnScaleWithSlug()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new FakeScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge_mm = scaleH0.Gauge.InMillimeters.Value,
                gauge_in = scaleH0.Gauge.InInches.Value,
                track_type = scaleH0.Gauge.TrackGauge.ToString(),
                description = scaleH0.Description,
                created = scaleH0.CreatedDate.ToDateTimeUtc(),
                version = scaleH0.Version
            });

            var exists = await Repository.ExistsAsync(scaleH0.Slug);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task ScalesRepository_Exists_ShouldReturnNullWhenScaleDoesNotExist()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var exists = await Repository.ExistsAsync(Slug.Of("not found"));

            exists.Should().BeFalse();
        }

        [Fact]
        public async Task ScalesRepository_GetScales_ShouldReturnAllScalesPaginated()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new FakeScale(new Guid("da2bfd8b-86e8-4531-b12b-7d442d4a4a75"), "H0");
            var scaleN = new FakeScale(new Guid("d3410d06-a2fb-4050-ad8c-4c4377fec4db"), "N");

            Database.Arrange.Insert(Tables.Scales,
                new
                {
                    scale_id = scaleH0.ScaleId.ToGuid(),
                    name = scaleH0.Name,
                    slug = scaleH0.Slug.ToString(),
                    ratio = scaleH0.Ratio.ToDecimal(),
                    gauge_mm = scaleH0.Gauge.InMillimeters.Value,
                    gauge_in = scaleH0.Gauge.InInches.Value,
                    track_type = scaleH0.Gauge.TrackGauge.ToString(),
                    description = scaleH0.Description,
                    created = scaleH0.CreatedDate.ToDateTimeUtc(),
                    version = scaleH0.Version
                },
                new
                {
                    scale_id = scaleN.ScaleId.ToGuid(),
                    name = scaleN.Name,
                    slug = scaleN.Slug.ToString(),
                    ratio = scaleN.Ratio.ToDecimal(),
                    gauge_mm = scaleN.Gauge.InMillimeters.Value,
                    gauge_in = scaleN.Gauge.InInches.Value,
                    track_type = scaleN.Gauge.TrackGauge.ToString(),
                    description = scaleN.Description,
                    created = scaleN.CreatedDate.ToDateTimeUtc(),
                    version = scaleN.Version
                });

            var result = await Repository.GetScalesAsync(new Page(1, 1));

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(1);
        }
    }
}
