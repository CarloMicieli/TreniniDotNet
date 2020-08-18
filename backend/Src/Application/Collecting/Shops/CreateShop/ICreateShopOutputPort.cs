using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public interface ICreateShopOutputPort : IStandardOutputPort<CreateShopOutput>
    {
        void ShopAlreadyExists(string shopName);
    }
}
