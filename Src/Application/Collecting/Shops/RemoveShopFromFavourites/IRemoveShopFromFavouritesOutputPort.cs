using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.RemoveShopFromFavourites
{
    public interface IRemoveShopFromFavouritesOutputPort : IStandardOutputPort<RemoveShopFromFavouritesOutput>
    {
        void ShopNotFound(ShopId shopId);
    }
}
