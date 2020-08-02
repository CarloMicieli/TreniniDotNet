using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public static class DatabaseSetupExtensions
    {
        public static void WithoutAnyWishlist(this DatabaseSetup databaseSetup)
        {
            databaseSetup.TruncateTable(Tables.WishlistItems);
            databaseSetup.TruncateTable(Tables.Wishlists);
        }
    }
}