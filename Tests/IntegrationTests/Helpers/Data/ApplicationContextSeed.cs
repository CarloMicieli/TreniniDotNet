using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.IntegrationTests.SeedData.Catalog;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public class ApplicationContextSeed
    {
        public static void SeedCatalog(ApplicationDbContext db)
        {
            db.SeedBrands();
            db.SeedScales();
            db.SeedRailways();
            db.SaveChanges();
        }
    }
}