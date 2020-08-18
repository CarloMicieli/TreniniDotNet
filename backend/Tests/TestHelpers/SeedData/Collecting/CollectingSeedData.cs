namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    // REMARKS: to make EF core testing happy, the data seed must produce the same value every time
    public static class CollectingSeedData
    {
        // REMARKS: due to init issues, Shops must come first
        public static Shops Shops = new Shops();

        public static Collections Collections = new Collections();

        public static ShopsFavourites ShopsFavourites = new ShopsFavourites();

        public static WishLists Wishlists = new WishLists();
    }
}
