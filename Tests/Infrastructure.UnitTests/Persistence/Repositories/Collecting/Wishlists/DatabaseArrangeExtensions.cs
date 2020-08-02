using System;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Wishlists
{
    public static class DatabaseArrangeExtensions
    {
        public static void WithOneWishlist(this DatabaseArrange databaseArrange, Wishlist wishlist)
        {
            databaseArrange.InsertOne(Tables.Wishlists, new
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
                    databaseArrange.InsertOne(Tables.WishlistItems, new
                    {
                        item_id = item.Id.ToGuid(),
                        wishlist_id = wishlist.Id.ToGuid(),
                        catalog_item_id = item.CatalogItem.Id.ToGuid(),
                        catalog_item_slug = item.CatalogItem.Slug,
                        priority = item.Priority.ToString(),
                        added_date = item.AddedDate.ToDateTimeUnspecified()
                    });
                }
            }
        }
    }
}