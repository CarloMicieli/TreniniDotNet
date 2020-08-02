using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Catalog.Scales
{
    public class ScalesRepositoryTests : RepositoryUnitTests<IScalesRepository>
    {
        public ScalesRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, unitOfWork => new ScalesRepository(unitOfWork))
        {
        }
        
        [Fact]
        public async Task ScalesRepository_Add_ShouldCreateANewScale()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = CatalogSeedData.Scales.ScaleH0();

            var scaleId = await Repository.AddAsync(scaleH0);

            scaleId.Should().NotBeNull();

            Database.Assert.RowInTable(Tables.Scales)
                .WithPrimaryKey(new
                {
                    scale_id = scaleH0.Id.ToGuid()
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

            var scaleH0 = CatalogSeedData.Scales.ScaleH0();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.Id.ToGuid(),
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

            await Repository.UpdateAsync(scaleH0.With(ratio: Ratio.Of(100M)));

            Database.Assert.RowInTable(Tables.Scales)
                .WithPrimaryKey(new
                {
                    scale_id = scaleH0.Id.ToGuid()
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

            var scaleH0 = CatalogSeedData.Scales.ScaleH0();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.Id.ToGuid(),
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
            scale?.Slug.Should().Be(scaleH0.Slug);
        }
        
        [Fact]
        public async Task ScalesRepository_Exists_ShouldReturnScaleWithSlug()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = CatalogSeedData.Scales.ScaleH0();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.Id.ToGuid(),
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

            var scaleH0 = CatalogSeedData.Scales.ScaleH0();
            var scaleN = CatalogSeedData.Scales.ScaleN();

            Database.Arrange.Insert(Tables.Scales,
                new
                {
                    scale_id = scaleH0.Id.ToGuid(),
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
                    scale_id = scaleN.Id.ToGuid(),
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

            var result = await Repository.GetAllAsync(new Page(1, 1));

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(1);
        }
    }
}