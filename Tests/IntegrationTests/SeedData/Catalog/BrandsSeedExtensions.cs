using System;
using TreniniDotNet.Common;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;

namespace TreniniDotNet.IntegrationTests.SeedData.Catalog
{
    public static class BrandsSeedExtensions
    {
        public static void SeedBrands(this ApplicationDbContext db)
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

            db.Brands.Add(new Brand
            {
                BrandId = new Guid("4b7a619b-65cc-41f5-a003-450537c85dea"),
                Name = "Roco",
                Slug = "roco",
                BrandKind = "Industrial",
                CompanyName = "Modelleisenbahn GmbH",
                WebsiteUrl = "http://www.roco.cc",
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Brands.Add(new Brand
            {
                BrandId = new Guid("ff9f5055-8ae7-4d58-a68f-0cee3adb6656"),
                Name = "Bemo",
                Slug = "bemo",
                BrandKind = "Industrial",
                CompanyName = "BEMO Modelleisenbahnen GmbH u. Co KG",
                WebsiteUrl = "https://www.bemo-modellbahn.de/",
                EmailAddress = "mail@bemo-modellbahn.de",
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Brands.Add(new Brand
            {
                BrandId = new Guid("c37f8ac1-991b-422c-b273-5c02efe2087e"),
                Name = "Brawa",
                Slug = "brawa",
                BrandKind = "Industrial",
                CompanyName = "BRAWA Artur Braun Modellspielwarenfabrik GmbH & Co. KG",
                WebsiteUrl = "https://www.brawa.de/",
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Brands.Add(new Brand
            {
                BrandId = new Guid("66fa5a39-7e47-471f-9f92-d2bb01258c31"),
                Name = "Märklin",
                Slug = Slug.Of("Märklin").ToString(),
                BrandKind = "Industrial",
                CompanyName = "Gebr. Märklin & Cie. GmbH",
                WebsiteUrl = "https://www.maerklin.de/",
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });

            db.Brands.Add(new Brand
            {
                BrandId = new Guid("2a916d99-953f-44d6-8115-7e72ca22b081"),
                Name = "Fleischmann",
                Slug = "fleischmann",
                BrandKind = "Industrial",
                CompanyName = "Modelleisenbahn München GmbH",
                WebsiteUrl = "https://www.fleischmann.de/",
                Version = 1,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
