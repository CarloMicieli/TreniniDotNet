using System;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public static class DatabaseTestHelpersExtensions
    {
        public static void ArrangeWithoutAnyWishlist(this DatabaseTestHelpers database)
        {
            database.Setup.TruncateTable(Tables.WishlistItems);
            database.Setup.TruncateTable(Tables.Wishlists);
        }

        public static void ArrangeWithOneWishlist(this DatabaseTestHelpers database, Wishlist wishlist)
        {
            database.ArrangeWithoutAnyWishlist();

            database.ArrangeCatalogData();

            database.Arrange.InsertOne(Tables.Wishlists, new
            {
                wishlist_id = wishlist.Id.ToGuid(),
                owner = wishlist.Owner.Value,
                slug = wishlist.Slug.Value,
                wishlist_name = wishlist.ListName,
                visibility = wishlist.Visibility.ToString(),
                created = DateTime.UtcNow
            });

            if (wishlist.Items.Count > 0)
            {
                foreach (var item in wishlist.Items)
                {
                    database.Arrange.InsertOne(Tables.WishlistItems, new
                    {
                        wishlist_item_id = item.Id.ToGuid(),
                        wishlist_id = wishlist.Id.ToGuid(),
                        catalog_item_id = item.CatalogItem.Id.ToGuid(),
                        priority = item.Priority.ToString(),
                        added_date = item.AddedDate.ToDateTimeUnspecified()
                    });
                }
            }
        }
    }
}