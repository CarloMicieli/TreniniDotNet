using System;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.IntegrationTests.SeedData.Catalog
{
    public static class ScalesSeedExtensions
    {
        public static void SeedScales(this ApplicationDbContext db)
        {
            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("7edfb586-218c-4997-8820-f61d3a81ce66"),
                Name = "H0",
                Slug = "h0",
                Gauge = 16.5M,
                Ratio = 87M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("f02ae69c-6a60-4fd4-bf5b-ac950e696361"),
                Name = "N",
                Slug = "n",
                Gauge = 9M,
                Ratio = 160M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("fb7ab3fc-5f15-4e2c-a8d3-7ef2e615dae8"),
                Name = "1",
                Slug = "1",
                Gauge = 45M,
                Ratio = 32M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("efc1fdb5-93aa-4a52-bbce-5ab67e92980c"),
                Name = "0",
                Slug = "0",
                Gauge = 32M,
                Ratio = 43.5M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("02790f5e-8edc-43f6-8ac1-4c906805d9ba"),
                Name = "Z",
                Slug = "z",
                Gauge = 6.5M,
                Ratio = 220M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("374f5bb7-e7d1-4995-aa34-072b6b6500f9"),
                Name = "TT",
                Slug = "tt",
                Gauge = 12M,
                Ratio = 120M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("0dd13f9d-d730-41bb-b4e9-33218ea14fbc"),
                Name = "H0m",
                Slug = "h0m",
                Gauge = 12M,
                Ratio = 87M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Narrow.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });

            db.Scales.Add(new Scale
            {
                ScaleId = new Guid("b5f2f033-a947-4b86-9d9e-52d7c1903ce0"),
                Name = "H0e",
                Slug = "h0e",
                Gauge = 9M,
                Ratio = 87M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Narrow.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });
        }
    }
}
