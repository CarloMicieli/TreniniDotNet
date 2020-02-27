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

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("452ecf0f-0999-443d-a333-7afc23542d38"),
                Name = "SNCB",
                Slug = "sncb",
                CompanyName = "Société Nationale des Chemins de fer belges",
                Country = "BE",
                Status = "Active",
                OperatingSince = null,
                OperatingUntil = null,
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("5cbccdd5-826d-4dc8-a0ff-d80572d43ac5"),
                Name = "SNCF",
                Slug = "sncf",
                CompanyName = "Société Nationale des Chemins de fer Français",
                Country = "FR",
                Status = "Active",
                OperatingSince = null,
                OperatingUntil = null,
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("fa307786-00a9-4257-9274-76f7a0c06fab"),
                Name = "RhB",
                Slug = "rhb",
                CompanyName = "Rhätische Bahn / Viafier retica",
                Country = "CH",
                Status = "Active",
                OperatingSince = null,
                OperatingUntil = null,
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("1c44e65f-bb53-4f14-a368-23daa5bfee05"),
                Name = "SBB",
                Slug = "sbb",
                CompanyName = "Schweizerische Bundesbahnen",
                Country = "CH",
                Status = "Active",
                OperatingSince = null,
                OperatingUntil = null,
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("f12a3c5b-21f0-4d96-baf0-7bbf67e85e93"),
                Name = "DB",
                Slug = "db",
                CompanyName = "Die Bahn AG",
                Country = "DE",
                Status = "Active",
                OperatingSince = null,
                OperatingUntil = null,
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
