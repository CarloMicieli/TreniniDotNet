using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Application.Collecting.Shops.AddShopToFavourites
{
    public interface IAddShopToFavouritesOutputPort : IStandardOutputPort<AddShopToFavouritesOutput>
    {
        void ShopNotFound(ShopId shopId);
    }
}
