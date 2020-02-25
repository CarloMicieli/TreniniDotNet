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
                Name = "n",
                Slug = "n",
                Gauge = 9M,
                Ratio = 160M,
                TrackGauge = Domain.Catalog.Scales.TrackGauge.Standard.ToString(),
                CreatedAt = DateTime.UtcNow,
                Version = 1
            });
        }
    }
}
