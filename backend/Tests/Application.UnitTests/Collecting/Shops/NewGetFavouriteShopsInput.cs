using TreniniDotNet.Application.Collecting.Shops.GetFavouriteShops;

namespace TreniniDotNet.Application.Collecting.Shops
{
    public static class NewGetFavouriteShopsInput
    {
        public static readonly GetFavouriteShopsInput Empty = With();

        public static GetFavouriteShopsInput With(string owner = null) =>
            new GetFavouriteShopsInput(owner);
    }
}
