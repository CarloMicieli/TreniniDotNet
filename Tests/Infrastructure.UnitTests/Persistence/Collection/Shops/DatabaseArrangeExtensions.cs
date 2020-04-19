using System;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Shops
{
    public static class DatabaseArrangeExtensions
    {
        public static void WithOneShop(this DatabaseArrange databaseArrange, IShop shop)
        {
            databaseArrange.InsertOne(Tables.Shops, new
            {
                shop_id = shop.ShopId.ToGuid(),
                name = shop.Name,
                slug = shop.Slug.Value,
                created = DateTime.UtcNow,
                version = 1
            });
        }
    }
}
