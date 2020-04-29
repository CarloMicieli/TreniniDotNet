using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public interface ICreateShopOutputPort : IOutputPortStandard<CreateShopOutput>
    {
        void ShopAlreadyExists(string shopName);
    }
}
