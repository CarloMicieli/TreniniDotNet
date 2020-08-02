using System;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Infrastructure.Database.Testing;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Shops
{
    public static class DatabaseArrangeExtensions
    {
        public static void WithOneShop(this DatabaseArrange databaseArrange, Shop shop)
        {
            databaseArrange.InsertOne(Tables.Shops, new
            {
                shop_id = shop.Id.ToGuid(),
                name = shop.Name,
                slug = shop.Slug.Value,
                created = DateTime.UtcNow,
                version = 1
            });
        }
    }
}