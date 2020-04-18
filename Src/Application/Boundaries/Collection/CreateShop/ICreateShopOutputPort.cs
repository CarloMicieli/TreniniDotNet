namespace TreniniDotNet.Application.Boundaries.Collection.CreateShop
{
    public interface ICreateShopOutputPort : IOutputPortStandard<CreateShopOutput>
    {
        void ShopAlreadyExists(string shopName);
    }
}
