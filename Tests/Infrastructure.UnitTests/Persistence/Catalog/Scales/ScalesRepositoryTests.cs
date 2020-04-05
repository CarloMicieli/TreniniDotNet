using Xunit;
using FluentAssertions;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Database.Testing;
using TreniniDotNet.Infrastracture.Dapper;
using NodaTime;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using System;
using TreniniDotNet.Common;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Scales
{
    public class ScalesRepositoryTests : RepositoryUnitTests<IScalesRepository>
    {
        public ScalesRepositoryTests(SqliteDatabaseFixture fixture)
            : base(fixture, CreateRepository)
        {
        }

        private static IScalesRepository CreateRepository(IDatabaseContext databaseContext, IClock clock) =>
            new ScalesRepository(databaseContext, new ScalesFactory(clock));

        [Fact]
        public async Task ScalesRepository_Add_ShouldCreateANewScale()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new TestScale();

            var scaleId = await Repository.Add(scaleH0);

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
                    gauge = scaleH0.Gauge.ToDecimal(MeasureUnit.Millimeters),
                    // track_type = scaleH0.TrackGauge.ToString(),
                    notes = scaleH0.Notes,
                    created_at = scaleH0.CreatedAt,
                    version = scaleH0.Version
                })
                .ShouldExists();
        }

        [Fact]
        public async Task ScalesRepository_GetBySlug_ShouldReturnScaleWithSlug()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new TestScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge = scaleH0.Gauge.ToDecimal(MeasureUnit.Millimeters),
                notes = scaleH0.Notes,
                track_type = scaleH0.TrackGauge.ToString(),
                created_at = scaleH0.CreatedAt,
                version = scaleH0.Version
            });

            var scale = await Repository.GetBySlug(scaleH0.Slug);

            scale.Should().NotBeNull();
            scale.Slug.Should().Be(scaleH0.Slug);
        }

        [Fact]
        public async Task ScalesRepository_GetByName_ShouldReturnScaleWithName()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new TestScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge = scaleH0.Gauge.ToDecimal(MeasureUnit.Millimeters),
                notes = scaleH0.Notes,
                track_type = scaleH0.TrackGauge.ToString(),
                created_at = scaleH0.CreatedAt,
                version = scaleH0.Version
            });

            var scale = await Repository.GetByName(scaleH0.Name);

            scale.Should().NotBeNull();
            scale.Name.Should().Be(scaleH0.Name);
        }

        [Fact]
        public async Task ScalesRepository_Exists_ShouldReturnScaleWithSlug()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new TestScale();
            Database.Arrange.InsertOne(Tables.Scales, new
            {
                scale_id = scaleH0.ScaleId.ToGuid(),
                name = scaleH0.Name,
                slug = scaleH0.Slug.ToString(),
                ratio = scaleH0.Ratio.ToDecimal(),
                gauge = scaleH0.Gauge.ToDecimal(MeasureUnit.Millimeters),
                notes = scaleH0.Notes,
                track_type = scaleH0.TrackGauge.ToString(),
                created_at = scaleH0.CreatedAt,
                version = scaleH0.Version
            });

            var exists = await Repository.Exists(scaleH0.Slug);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task ScalesRepository_Exists_ShouldReturnNullWhenScaleDoesNotExist()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var exists = await Repository.Exists(Slug.Of("not found"));

            exists.Should().BeFalse();
        }

        [Fact]
        public async Task ScalesRepository_GetAll_ShouldReturnAllScales()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new TestScale(new Guid("da2bfd8b-86e8-4531-b12b-7d442d4a4a75"), "H0");
            var scaleN = new TestScale(new Guid("d3410d06-a2fb-4050-ad8c-4c4377fec4db"), "N");

            Database.Arrange.Insert(Tables.Scales,
                new
                {
                    scale_id = scaleH0.ScaleId.ToGuid(),
                    name = scaleH0.Name,
                    slug = scaleH0.Slug.ToString(),
                    ratio = scaleH0.Ratio.ToDecimal(),
                    gauge = scaleH0.Gauge.ToDecimal(MeasureUnit.Millimeters),
                    notes = scaleH0.Notes,
                    track_type = scaleH0.TrackGauge.ToString(),
                    created_at = scaleH0.CreatedAt,
                    version = scaleH0.Version
                },
                new
                {
                    scale_id = scaleN.ScaleId.ToGuid(),
                    name = scaleN.Name,
                    slug = scaleN.Slug.ToString(),
                    ratio = scaleN.Ratio.ToDecimal(),
                    gauge = scaleN.Gauge.ToDecimal(MeasureUnit.Millimeters),
                    notes = scaleN.Notes,
                    track_type = scaleN.TrackGauge.ToString(),
                    created_at = scaleN.CreatedAt,
                    version = scaleN.Version
                });

            var scales = await Repository.GetAll();

            scales.Should().NotBeNull();
            scales.Should().HaveCount(2);
        }


        [Fact]
        public async Task ScalesRepository_GetScales_ShouldReturnAllScalesPaginated()
        {
            Database.Setup.TruncateTable(Tables.Scales);

            var scaleH0 = new TestScale(new Guid("da2bfd8b-86e8-4531-b12b-7d442d4a4a75"), "H0");
            var scaleN = new TestScale(new Guid("d3410d06-a2fb-4050-ad8c-4c4377fec4db"), "N");

            Database.Arrange.Insert(Tables.Scales,
                new
                {
                    scale_id = scaleH0.ScaleId.ToGuid(),
                    name = scaleH0.Name,
                    slug = scaleH0.Slug.ToString(),
                    ratio = scaleH0.Ratio.ToDecimal(),
                    gauge = scaleH0.Gauge.ToDecimal(MeasureUnit.Millimeters),
                    notes = scaleH0.Notes,
                    track_type = scaleH0.TrackGauge.ToString(),
                    created_at = scaleH0.CreatedAt,
                    version = scaleH0.Version
                },
                new
                {
                    scale_id = scaleN.ScaleId.ToGuid(),
                    name = scaleN.Name,
                    slug = scaleN.Slug.ToString(),
                    ratio = scaleN.Ratio.ToDecimal(),
                    gauge = scaleN.Gauge.ToDecimal(MeasureUnit.Millimeters),
                    notes = scaleN.Notes,
                    track_type = scaleN.TrackGauge.ToString(),
                    created_at = scaleN.CreatedAt,
                    version = scaleN.Version
                });

            var result = await Repository.GetScales(new Page(1, 1));

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(1);
        }
    }

    public class TestScale : IScale
    {
        public TestScale()
            : this(new Guid("66c9e10f-0382-4e4b-8986-1f41bc883347"), "H0")
        {
        }

        public TestScale(Guid id, string name)
        {
            _scaleId = new ScaleId(id);
            _name = name;
        }

        private readonly ScaleId _scaleId;
        private readonly string _name;

        public ScaleId ScaleId => _scaleId;

        public Slug Slug => Slug.Of(_name);

        public string Name => _name;

        public Gauge Gauge => Gauge.OfMillimiters(16.5M);

        public TrackGauge TrackGauge => TrackGauge.Standard;

        public string Notes => null;

        public DateTime? CreatedAt => new DateTime(2020, 11, 25);

        public int? Version => 42;

        public Ratio Ratio => Ratio.Of(87M);

        public IScaleInfo ToScaleInfo()
        {
            throw new NotImplementedException();
        }
    }
}
