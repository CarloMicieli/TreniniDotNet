using System;
using TreniniDotNet.Domain.Collection.Wishlists;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Wishlists
{
    public static class DatabaseArrangeExtensions
    {
        public static void WithOneWishlist(this DatabaseArrange databaseArrange, IWishlist wishlist)
        {
            databaseArrange.InsertOne(Tables.Wishlists, new
            {
                wishlist_id = wishlist.WishlistId.ToGuid(),
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
                    databaseArrange.InsertOne(Tables.WishlistItems, new
                    {
                        item_id = item.ItemId.ToGuid(),
                        wishlist_id = wishlist.WishlistId.ToGuid(),
                        catalog_item_id = item.CatalogItem.CatalogItemId.ToGuid(),
                        catalog_item_slug = item.CatalogItem.Slug.Value,
                        priority = item.Priority.ToString(),
                        added_date = item.AddedDate.ToDateTimeUnspecified()
                    });
                }
            }
        }
    }
}
