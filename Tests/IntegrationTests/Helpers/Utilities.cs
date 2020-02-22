using System;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;

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

            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApplicationDbContext db)
        {
            db.Brands.RemoveRange(db.Brands);
            //db.Railways.RemoveRange(db.Railways);
            //db.Scales.RemoveRange(db.Scales);
            InitializeDbForTests(db);
        }
    }

}