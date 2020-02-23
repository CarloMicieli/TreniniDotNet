using System;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;

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
                EmailAddress = "mail@acmetreni.com"
            });

            db.Railways.Add(new Railway
            {
                RailwayId = new Guid("e8d33cd3-f36b-4622-90d1-76b450e0f313"),
                Name = "FS",
                Slug = "fs",
                CompanyName = "Ferrovie dello stato",
                Country = "IT",
                Status = "Active",
                Version = 1,
                OperatingSince = new DateTime(1905, 7, 1),
                OperatingUntil = null
            });

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Brands.RemoveRange(db.Brands);
            db.Railways.RemoveRange(db.Railways);
            InitializeDbForTests(db);
        }
    }
}