using System;
using TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public static class NewRemoveShopFromFavouritesInput
    {
        public static readonly RemoveShopFromFavouritesInput Empty = With();

        public static RemoveShopFromFavouritesInput With(string owner = null, Guid? shopId = null) =>
            new RemoveShopFromFavouritesInput(owner, shopId ?? Guid.Empty);
    }
}
