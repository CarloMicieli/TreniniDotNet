namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    // REMARKS: to make EF core testing happy, the data seed must produce the same value every time
    public static class CatalogSeedData
    {
        public static readonly Brands Brands = new Brands();

        public static readonly Railways Railways = new Railways();

        public static readonly Scales Scales = new Scales();

        // REMARKS: due to init issue, this must come last.
        public static readonly CatalogItems CatalogItems = new CatalogItems();
    }
}
