using System;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.Brands.Add(new Brand
            {
                BrandId = new Guid("9ed9f089-2053-4a39-b669-a6d603080402"),
                Name = "ACME",
                Slug = "acme",
                BrandKind = "Industrial",
                CompanyName = "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl = "http://www.acmetreni.com",
                EmailAddress = "mail@acmetreni.com",
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313"),
                Name = "FS",
                Slug = "fs",
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                OperatingSince = new DateTime(1905, 7, 1),
                OperatingUntil = null,
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

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

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Brands.RemoveRange(db.Brands);
            db.Railways.RemoveRange(db.Railways);
            db.Scales.RemoveRange(db.Scales);
            InitializeDbForTests(db);
        }
    }
}