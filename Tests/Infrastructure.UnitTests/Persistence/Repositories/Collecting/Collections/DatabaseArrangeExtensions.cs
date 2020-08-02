using System;
using System.Linq;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
    public static class DatabaseArrangeExtensions
    {
        public static void ArrangeDatabaseWithOneCollectionItem(this DatabaseArrange databaseArrange,
            Collection collection)
        {
            var item = collection.Items.First();
            
            databaseArrange.InsertOne(Tables.Shops, new
            {
                shop_id = item.PurchasedAt.Id.ToGuid(),
                slug = item.PurchasedAt.Slug,
                name = item.PurchasedAt.ToString(),
                created = DateTime.UtcNow
            });

            databaseArrange.InsertOne(Tables.CollectionItems, new
            {
                item_id = item.Id.ToGuid(),
                collection_id = collection.Id.ToGuid(),
                catalog_item_id = item.CatalogItem.Id.ToGuid(),
                catalog_item_slug = item.CatalogItem.Slug,
                price = item.Price.Amount,
                currency = item.Price.Currency,
                condition = item.Condition.ToString(),
                shop_id = item.PurchasedAt.Id.ToGuid()
            });
        }
    }
}