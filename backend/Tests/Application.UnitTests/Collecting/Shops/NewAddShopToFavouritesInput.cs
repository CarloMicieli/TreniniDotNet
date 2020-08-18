using System;
using TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public static class NewAddShopToFavouritesInput
    {
        public static readonly AddShopToFavouritesInput Empty = With();

        public static AddShopToFavouritesInput With(string owner = null, Guid? id = null) =>
            new AddShopToFavouritesInput(owner, id ?? Guid.Empty);
    }
}
