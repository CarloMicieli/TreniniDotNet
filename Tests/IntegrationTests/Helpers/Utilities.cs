using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.IntegrationTests.SeedData.Catalog;

namespace TreniniDotNet.IntegrationTests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            db.SeedBrands();
            db.SeedScales();
            db.SeedRailways();
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