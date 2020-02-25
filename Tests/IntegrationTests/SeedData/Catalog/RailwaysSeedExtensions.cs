using System;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;

namespace TreniniDotNet.IntegrationTests.SeedData.Catalog
{
    public static class RailwaysSeedExtensions
    {
        public static void SeedRailways(this ApplicationDbContext db)
        {
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
        }
    }
}
