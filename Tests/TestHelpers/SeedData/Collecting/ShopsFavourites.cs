using System.Collections.Generic;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.TestHelpers.SeedData.Collecting
{
    public class ShopsFavourites
    {
        public ShopsFavourites()
        {
            var shops = new Shops();
            _all = new List<ShopFavourite>()
            {
                new ShopFavourite()
                {
                    Owner = new Owner("George"),
                    ShopId = shops.NewModellbahnshopLippe().Id
                }
            };
        }

        private readonly IList<ShopFavourite> _all;

        public IList<ShopFavourite> All() => _all;
    }

    public sealed class ShopFavourite
    {
        public Owner Owner { set; get; }
        public ShopId ShopId { set; get; }
        public Shop Shop { set; get; }
    }
}
